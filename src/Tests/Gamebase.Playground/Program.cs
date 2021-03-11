namespace Gamebase.Playground
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Scraping;
    using Services.Seeding;

    class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration settings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<GamebaseDbContext>(options => options.UseSqlServer(settings.GetConnectionString("DefaultConnection")));

            var serviceProvider = services.BuildServiceProvider();
            var appDbContext = serviceProvider.GetService<GamebaseDbContext>();

            var apiConfigSettings = new ConfigSettings
            {
                ClientId = settings.GetSection("ConfigSettings:Client-ID").Value,
                Authorization = settings.GetSection("ConfigSettings:Authorization").Value,
            };

            var seeder = new Seeder(apiConfigSettings, appDbContext);
            var ids = new List<int>();
            for (int i = 1; i <= 500; i++)
            {
                ids.Add(i);
            }
            //character with id 114 has an invalid collection
            ids.RemoveAt(115);

            await seeder.SeedGames(ids);
            Console.WriteLine("All 500 games seeded.");
            await seeder.SeedCharacters(ids);
            Console.WriteLine("All 500 characters and their corresponding games seeded.");
        }
    }
}
