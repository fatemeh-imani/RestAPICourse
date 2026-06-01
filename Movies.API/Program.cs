using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Auth;
using Movies.API.Middleware;
using Movies.Applications;
using Movies.Applications.DataBaces.DBContext;
using Movies.Applications.DataBaces.Seed;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication(builder.Configuration);
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

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(AuthConstants.AdminUserPolicyName
        , p => p.RequireClaim(AuthConstants.AdminUserClaimName , "true" ));

    x.AddPolicy(AuthConstants.TrustedMemberPolicyName
        , p => p.RequireAssertion(c =>
        c.User.HasClaim(m => m is { Type: AuthConstants.AdminUserClaimName, Value: "true" }) ||
        c.User.HasClaim(m => m is { Type: AuthConstants.TrustedMemberClaimName, Value: "true"})));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await USerSeeder.EnsureAsync(scope.ServiceProvider);
}

app.UseMiddleware<ValidationMappingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RestDBContext>();
    await DbSeeder.SeedAsync(context);
}

app.Run();
