namespace Gamebase.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Services;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Web.ViewModels.Games;
    using Xunit;

    public class GamesServiceTest
    {
        private readonly GamebaseDbContext context;
        private readonly GamesService gamesService;
        private readonly TestsSettings settings = new TestsSettings();

        public GamesServiceTest()
        {
            this.context = settings.InitializeDatabase().GetAwaiter().GetResult();
            this.gamesService = new GamesService(context);
        }

        [Fact]
        public async Task CheckIfGetAllReturnsAllEntitiesInDatabase()
        {
            await this.settings.SeedGames();

            //Arrange
            var games = this.context.Games.Count();

            //Assert
            Assert.Equal(5, games);
        }
    }
}
