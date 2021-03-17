namespace Gamebase.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Data;
    using Data.Services;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Settings;
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
            this.settings.SeedGames(15).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task CheckIfSeedGamesAddsGamesToInMemoryDb()
        {
            var gamesCount = this.context.Games.Count();

            Assert.Equal(15, gamesCount);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        //we have seeded 15 games => 2 pages => 3 is an invalid parameter
        [InlineData(3)]
        public void InvalidPageNumberOnAllPageShouldReturnNull(int page)
        {
            var games = this.gamesService.GetAll(page);
            Assert.Null(games);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void InvalidPageNumberOnDetailsPageShouldReturnNull(int page)
        {
            var game = this.gamesService.GetSingle(page);
            Assert.Null(game);
        }
    }
}
