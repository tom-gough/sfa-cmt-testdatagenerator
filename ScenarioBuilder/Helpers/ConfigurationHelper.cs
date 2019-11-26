using Microsoft.Extensions.Configuration;

namespace ScenarioBuilder.Helpers
{
    public static class ConfigurationHelper
    {
        public static ApplicationConfiguration Configuration { get; private set; }

        public static void Initialise(string outputPath)
        {
            Configuration = new ApplicationConfiguration();
            var iConfig = GetIConfigurationRoot(outputPath);
            var section= iConfig.GetSection("ApplicationConfiguration");
            section.Bind(Configuration);
        }

        private static IConfiguration GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }

    public class ApplicationConfiguration
    {
        public string DbConnectionString { get; set; }
        public string HashSalt { get; set; }
        public string HashAlphabet { get; set; }
        public string AccountLegalEntityHashSalt { get; set; }
        public string AccountLegalEntityHashAlphabet { get; set; }
    }
}
