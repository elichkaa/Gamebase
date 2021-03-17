namespace Gamebase.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Data.Services;
    using Data.Services.Contracts;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Scraping;
    using Seeding;

    public class TestsSettings
    {
        private SqliteConnection connection;
        private GamebaseDbContext dbContext;
        private ConfigSettings apiConfigSettings;

        public async Task<GamebaseDbContext> InitializeDatabase()
        {
            var services = new ServiceCollection();

            IConfiguration settings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            this.connection = new SqliteConnection("DataSource=:memory:");
            this.connection.Open();
            var options = new DbContextOptionsBuilder<GamebaseDbContext>().UseSqlite(this.connection);
            this.dbContext = new GamebaseDbContext(options.Options);

            await this.dbContext.Database.EnsureCreatedAsync();

            this.apiConfigSettings = new ConfigSettings
            {
                ClientId = settings.GetSection("ConfigSettings:Client-ID").Value,
                Authorization = settings.GetSection("ConfigSettings:Authorization").Value,
            };
            return this.dbContext;
        }

        public async Task SeedGames()
        {
            var seeder = new Seeder(apiConfigSettings, dbContext);
            var ids = new List<int>();
            for (int i = 1; i <= 5; i++)
            {
                ids.Add(i);
            }

            await seeder.SeedGames(ids);
            Console.WriteLine("All 5 games seeded.");
            await dbContext.SaveChangesAsync();
        }

        public async Task SeedCharacters()
        {
            var seeder = new Seeder(apiConfigSettings, dbContext);
            var ids = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                ids.Add(i);
            }

            await seeder.SeedCharacters(ids);
            Console.WriteLine("All 10 characters and their corresponding games seeded.");
            await dbContext.SaveChangesAsync();
        }
    }
}
