using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;



namespace Movies.API.Swagger
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            // تنظیم وضعیت Deprecated (منسوخ شده)
            operation.Deprecated |= apiDescription.IsDeprecated();

            // حذف کدهای مربوط به IsDefault که خطا می‌داد
            // به جای آن، فقط روی پارامترها تمرکز می‌کنیم که بخش اصلی کارکرد Swagger هستند
            if (operation.Parameters == null) return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions
                    .FirstOrDefault(p => p.Name == parameter.Name);

                if (description == null) continue;

                parameter.Description ??= description.ModelMetadata?.Description;

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    // به جای استفاده از فکتوری، مقدار را به صورت دستی بر اساس نوع (Type) ایجاد می‌کنیم
                    var type = description.ParameterDescriptor?.ParameterType;
                    if (type == typeof(string))
                    {
                        parameter.Schema.Default = new Microsoft.OpenApi.Any.OpenApiString(description.DefaultValue.ToString());
                    }
                    else if (type == typeof(int))
                    {
                        parameter.Schema.Default = new Microsoft.OpenApi.Any.OpenApiInteger((int)description.DefaultValue);
                    }
                    else if (type == typeof(bool))
                    {
                        parameter.Schema.Default = new Microsoft.OpenApi.Any.OpenApiBoolean((bool)description.DefaultValue);
                    }
                    // در صورت نیاز می‌توانید برای سایر تایپ‌ها (مثل Double یا Long) هم شرط بگذارید
                }

                parameter.Required |= description.IsRequired;
            }
        }

    }
}
