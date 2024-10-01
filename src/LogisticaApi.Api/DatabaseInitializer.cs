using RetailProductMicroservice.Api.Configuration;
using RetailProductMicroservice.Infrastructure.Data;

namespace RetailProductMicroservice.Api.Services
{
    public class DatabaseInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger, AppSettings appSettings)
        {
            _serviceProvider = serviceProvider;
            _appSettings = appSettings;
            _logger = logger;
        }

        public void Initialize()
        {
            var updateDatabaseOnInit = _appSettings.UpdateDatabaseOnInit;

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