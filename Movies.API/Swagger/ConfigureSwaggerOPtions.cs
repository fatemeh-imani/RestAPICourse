using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

public class ConfigureSwaggerOptions 
    //: IConfigureOptions<SwaggerGenOptions>
{
    //private readonly IApiVersionDescriptionProvider _provider;

    //public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    //{
    //    _provider = provider;
    //}

    //public void Configure(SwaggerGenOptions options)
    //{
    //    // اضافه کردن تنظیمات امنیتی JWT برای *همه* نسخه‌ها
    //    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //    {
    //        Name = "Authorization",
    //        Type = SecuritySchemeType.Http,
    //        Scheme = "Bearer",
    //        BearerFormat = "JWT",
    //        In = ParameterLocation.Header,
    //        Description = "لطفاً توکن JWT را وارد کنید"
    //    });

    //    // اضافه کردن الزامات امنیتی برای *همه* نسخه‌ها
    //    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //    {
    //        {
    //            new OpenApiSecurityScheme
    //            {
    //                Reference = new OpenApiReference
    //                {
    //                    Type = ReferenceType.SecurityScheme,
    //                    Id = "Bearer"
    //                }
    //            },
    //            new List<string>() // این لیست خالی است چون برای Bearer نیازی به لیست API ها نداریم
    //        }
    //    });

    //    // تنظیمات برای هر نسخه از API
    //    foreach (var description in _provider.ApiVersionDescriptions)
    //    {
    //        options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    //    }
    //}

    //private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription versionDescription)
    //{
    //    var info = new OpenApiInfo()
    //    {
    //        Title = "My API",
    //        Version = versionDescription.ApiVersion.ToString(),
    //        Description = "API Documentation"
    //    };

    //    if (versionDescription.IsDeprecated)
    //    {
    //        info.Description += " This API version has been deprecated.";
    //    }

    //    return info;
    //}
}
