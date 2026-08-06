using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Auth;
using Movies.API.Middleware;
using Movies.Applications;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Seed;
using Movies.Applications.Health;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddHealthChecks()
       .AddCheck<DatabaseHealthCkeck>(DatabaseHealthCkeck.Name);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});


// ===================== JWT Authentication =====================
var jwtKey = builder.Configuration["Jwt:Key"];
if (!string.IsNullOrWhiteSpace(jwtKey))
{
    var key = Encoding.UTF8.GetBytes(jwtKey);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

       
    });
}
// ============================================================================

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthConstants.AdminUserPolicyName,
        p => p.RequireClaim(AuthConstants.AdminUserClaimName, "true"));

    options.AddPolicy(AuthConstants.TrustedMemberPolicyName,
        p => p.RequireAssertion(c =>
            c.User.HasClaim(m => m.Type == AuthConstants.AdminUserClaimName && m.Value == "true") ||
            c.User.HasClaim(m => m.Type == AuthConstants.TrustedMemberClaimName && m.Value == "true")));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await USerSeeder.EnsureAsync(scope.ServiceProvider);
    var context = scope.ServiceProvider.GetRequiredService<RestDBContext>();
    await DbSeeder.SeedAsync(context);
}


app.UseMiddleware<ValidationMappingMiddleware>();

app.MapHealthChecks("_health");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
