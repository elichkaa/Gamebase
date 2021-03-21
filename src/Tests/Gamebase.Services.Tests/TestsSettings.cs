namespace Gamebase.Services.Tests.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Data;
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

        public async Task<GamebaseDbContext> InitializeDatabase()
        {
            this.connection = new SqliteConnection("DataSource=:memory:");
            this.connection.Open();
            var options = new DbContextOptionsBuilder<GamebaseDbContext>().UseSqlite(this.connection);
            this.dbContext = new GamebaseDbContext(options.Options);

            await this.dbContext.Database.EnsureDeletedAsync();
            await this.dbContext.Database.EnsureCreatedAsync();

            return this.dbContext;
        }

        public async Task SeedGames(int count)
        {
            var games = new List<Game>();
            for (int i = 1; i <= count; i++)
            {
                DateTime? dateTime = new DateTime(2020, 12, i);
                games.Add(new Game()
                {
                    Id = i,
                    Name = $"Game{i}",
                    FirstReleaseDate=dateTime,
                    Developers = new List<GamesDevelopers>()
                    {
                        new GamesDevelopers()
                        {
                            Developer = new Developer()
                            {
                                Id = i,
                                Name =  $"Developer{i}"
                            }
                        }
                    }
                });
            }

            await dbContext.Games.AddRangeAsync(games);
            await dbContext.SaveChangesAsync();
        }

        public async Task SeedGameModes()
        {
            var gameModes = new List<GameMode>();

            gameModes.Add(new GameMode()
            {
                Id = 1,
                Name = $"Single player"
            });
            gameModes.Add(new GameMode()
            {
                Id = 2,
                Name = $"Multiplayer"
            });
            gameModes.Add(new GameMode()
            {
                Id = 3,
                Name = $"Coop"
            });
            gameModes.Add(new GameMode()
            {
                Id = 4,
                Name = $"Split screen"
            });
            gameModes.Add(new GameMode()
            {
                Id = 5,
                Name = $"Massively Multiplayer"
            });

            await dbContext.GameModes.AddRangeAsync(gameModes);
            await dbContext.SaveChangesAsync();
        }

        public async Task SeedCharacters(int count)
        {
            var characters = new List<Character>();
            for (int i = 1; i <= count; i++)
            {
                characters.Add(new Character()
                {
                    Id = i,
                    Name = $"Character{i}"
                });
            }

            await dbContext.Characters.AddRangeAsync(characters);
            await dbContext.SaveChangesAsync();
        }
    }
}
