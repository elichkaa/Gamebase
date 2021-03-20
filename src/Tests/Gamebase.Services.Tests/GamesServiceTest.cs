namespace Gamebase.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Data;
    using Data.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Models;
    using Settings;
    using Web.InputModels.AddDelete;
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
            this.settings.SeedGameModes().GetAwaiter().GetResult();
        }

        [Fact]
        public void CheckIfSeedGamesAddsGamesToInMemoryDb()
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

        [Fact]
        public void AddGameTest()
        {
            string path = @"..\..\..\img\test.jpg";
            using var stream = new MemoryStream(File.ReadAllBytes(path).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", path.Split(@"\").Last());

            var gameInput = new AddGameInputModel
            {
                Name = "GTA",
                Cover = formFile,
                Storyline = "The biggest, most dynamic and most diverse open world ever created, Grand Theft Auto V blends storytelling...",
                Summary = "The biggest, most dynamic and most diverse open world ever created, Grand Theft Auto V blends storytelling...",
                FirstReleaseDate = new DateTime(2013, 9, 17),
                DeveloperNames = "Rockstar Games",
                CollectionName = "GTA",
                GameEngineNames = "RAGE",
                GameModeIds = new List<int>{1,2},
                GenreNames = "Adventure,Racing,Shooter",
                KeywordNames = "shooter,multiplayer",
                PlatformNames = "PC,PS4",
                Screenshots = new List<IFormFile>()
                {
                    formFile,formFile
                },
                CharacterNames = "Michael,Trevor"
            };

            this.gamesService.AddGame(gameInput, new ApplicationUser(),
                $"{Directory.GetCurrentDirectory()}/img/");

            Assert.Equal(16, this.context.Games.Count());
            Assert.Equal(1, this.context.Covers.Count());
            Assert.Equal(1, this.context.Developers.Count());
            Assert.Equal(1, this.context.GamesDevelopers.Count());
            Assert.Equal(1, this.context.Collections.Count());
            Assert.Equal(1, this.context.GameEngines.Count());
            Assert.Equal(1, this.context.GamesEngines.Count());
            Assert.Equal(2, this.context.GamesModes.Count());
            Assert.Equal(3, this.context.Genres.Count());
            Assert.Equal(3, this.context.GameGenres.Count());
            Assert.Equal(2, this.context.Keywords.Count());
            Assert.Equal(2, this.context.GamesKeywords.Count());
            Assert.Equal(2, this.context.Platforms.Count());
            Assert.Equal(2, this.context.GamePlatforms.Count());
            Assert.Equal(2, this.context.Screenshots.Count());
            Assert.Equal(2, this.context.Characters.Count());
            Assert.Equal(2, this.context.GameCharacters.Count());
        }
    }
}
