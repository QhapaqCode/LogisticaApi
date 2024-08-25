using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RetailProductMicroservice.Infrastructure.Data;

namespace RetailProductMicroservice.Api.Services
{
    public class DatabaseInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(IServiceProvider serviceProvider, IConfiguration configuration, ILogger<DatabaseInitializer> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _logger = logger;
        }

        public void Initialize()
        {
            var updateDatabaseOnInit = _configuration.GetValue<string>("UpdateDatabaseOnInit");

            if (updateDatabaseOnInit == "Active")
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<RetailProductContext>();

                    try
                    {
                        if (context.Database.EnsureCreated())
                        {
                            _logger.LogInformation("Database created successfully.");
                        }
                        else
                        {
                            _logger.LogInformation("Database already exists.");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred while creating the database.");
                    }
                }
            }
        }
    }
}