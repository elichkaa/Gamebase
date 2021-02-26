namespace Gamebase.Scraper
{
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;
    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var seeder = new Seeder(configuration);
            await seeder.SeedGames();
        }
    }
}
