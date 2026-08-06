using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Movies.Applications.DataBaces.DBContext;

namespace Movies.Applications.Health
{
    public class DatabaseHealthCkeck(RestDBContext _context , ILogger<DatabaseHealthCkeck> _logger) : IHealthCheck
    {
        public const string Name = "Database";
        public async Task<HealthCheckResult> CheckHealthAsync(
                     HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("در حال بررسی وضعیت دیتابیس");
            


            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

                if (canConnect)
                {
                    _logger.LogInformation("دیتابیس در دسترس است.");
                    return HealthCheckResult.Healthy("Database is healthy.");
                }

                _logger.LogWarning("دیتابیس در دسترس نیست!");
                return HealthCheckResult.Unhealthy("Database is unhealthy.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطایی در هنگام بررسی سلامت دیتابیس رخ داد.");
                return HealthCheckResult.Unhealthy("Database connection failed due to an exception.", ex);
            }
        }
    }
}
    

