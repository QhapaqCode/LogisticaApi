namespace RetailProductMicroservice.Api.Configuration
{
    public class AppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DefaultConnection => Environment.GetEnvironmentVariable("DEFAULT_CONNECTION") ?? _configuration.GetConnectionString("DefaultConnection");

        public string UpdateDatabaseOnInit => _configuration.GetValue<string>("UpdateDatabaseOnInit");

        public bool UseInMemoryDatabase => _configuration.GetValue<bool>("UseInMemoryDatabase");
    }
}