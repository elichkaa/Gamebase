namespace Gamebase.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Data;
    using Data.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Models;
    using Settings;
    using Web.InputModels.AddDelete;
    using Xunit;

    public class GamesServiceTest
    {
        private readonly GamebaseDbContext context;
        private readonly GamesService gamesService;
        private readonly TestsSettings settings = new TestsSettings();
        private readonly string imgPath = @"..\..\..\img\test.jpg";

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
        public void GetAllRetunsAllGames()
        {
            var gamesOnFirstPage = this.gamesService.GetAll(1);
            Assert.Equal(10, gamesOnFirstPage.Count);

            var gamesOnSecondPage = this.gamesService.GetAll(2);
            Assert.Equal(5, gamesOnSecondPage.Count);

            var counter = 1;
            foreach (var game in gamesOnFirstPage)
            {
                Assert.Equal($"Game{counter}", gamesOnFirstPage.ToList()[counter - 1].Name);
                counter++;
            }
            foreach (var game in gamesOnSecondPage)
            {
                Assert.Equal($"Game{counter}", gamesOnSecondPage.ToList()[counter - 11].Name);
                counter++;
            }

        }

        [Fact]
        public void AddGameTest()
        {
            //Arrange
            using var stream = new MemoryStream(File.ReadAllBytes(imgPath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", imgPath.Split(@"\").Last());
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

            //Act
            this.gamesService.AddGame(gameInput, new ApplicationUser(),
                $"{Directory.GetCurrentDirectory()}/img/");

            //Assert
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

        [Fact]
        public void AddGameWithoutName()
        {
            //Arrange
            using var stream = new MemoryStream(File.ReadAllBytes(imgPath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", imgPath.Split(@"\").Last());
            var gameInput = new AddGameInputModel
            {
                Name = null,
                Cover = formFile
            };

            //Act
            Action act = () => this.gamesService.AddGame(gameInput, new ApplicationUser(),
                $"{Directory.GetCurrentDirectory()}/img/");
            
            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Invalid input. Game name, cover and at least one game mode are required.", exception.Message);
        }

        [Fact]
        public void AddGameWithoutCover()
        {
            //Arrange
            var gameInput = new AddGameInputModel
            {
                Name = "GTA",
                Cover = null
            };

            //Act
            Action act = () => this.gamesService.AddGame(gameInput, new ApplicationUser(),
                $"{Directory.GetCurrentDirectory()}/img/");

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Invalid input. Game name, cover and at least one game mode are required.", exception.Message);
        }

        [Fact]
        public void AddGameWithoutGamemodes()
        {
            //Arrange
            using var stream = new MemoryStream(File.ReadAllBytes(imgPath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", imgPath.Split(@"\").Last());
            var gameInput = new AddGameInputModel
            {
                Name = "GTA",
                Cover = formFile,
                GameModeIds = null
            };

            //Act
            Action act = () => this.gamesService.AddGame(gameInput, new ApplicationUser(),
                $"{Directory.GetCurrentDirectory()}/img/");

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Invalid input. Game name, cover and at least one game mode are required.", exception.Message);
        }

        [Fact]
        public void AddGameWithNameThatAlreadyExists()
        {
            //Arrange
            using var stream = new MemoryStream(File.ReadAllBytes(imgPath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", imgPath.Split(@"\").Last());
            var gameInput = new AddGameInputModel
            {
                Name = "Game1",
                Cover = formFile,
                GameModeIds = new List<int>(){1}
            };

            //Act
            Action act = () => this.gamesService.AddGame(gameInput, new ApplicationUser(),
                $"{Directory.GetCurrentDirectory()}/img/");

            //Assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Game with this name already exists", exception.Message);
        }

        [Fact]
        public void DeleteGameTest()
        {
            //Arrange
            int gameId = 15;

            //Act
            this.gamesService.DeleteGame(gameId);

            //Assert
            Assert.Equal(14, this.context.Games.Count());
        }

        [Fact]
        public void DeleteGameThrowsExceptionIfGameIdIsInvalid()
        {
            //Arrange
            int gameId = 20;

            //Act
            Action act = () => this.gamesService.DeleteGame(gameId);

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Invalid Game Id (Parameter 'gameId')", exception.Message);
            Assert.Equal(15, this.context.Games.Count());
        }

        [Fact]
        public void GetGamesByUserReturnsCorrectGames()
        {
            //Arrange
            using var stream = new MemoryStream(File.ReadAllBytes(imgPath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", imgPath.Split(@"\").Last());

            var user = new ApplicationUser();
            var gameInput = new AddGameInputModel
            {
                Name = "GTA",
                Cover = formFile,
                GameModeIds = new List<int>() {1}
            };

            //Act
            this.gamesService.AddGame(gameInput, user,
                $"{Directory.GetCurrentDirectory()}/img/");
            var games = this.gamesService.GetGamesByUser(user.Id);

            //Assert
            Assert.Equal(1, games.Count);
            Assert.Equal("GTA", games.ToList()[0].Name);
        }

        [Fact]
        public void GetGamesByUserThrowsExceptionIfUserIdIsNotValid()
        {
            //Act
            Action act = () => this.gamesService.GetGamesByUser(new ApplicationUser().Id);

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Invalid UserId", exception.Message);
        }

        [Fact]
        public void GetGameModesTest()
        {
            //Act
            var gameModes = this.gamesService.GetGameModes();

            //Assert
            Assert.Equal(5, gameModes.Count);
        }

        [Fact]
        public void CheckIfEntityExistsReturnsTrueIfEntityExists()
        {
            //Assert
            var gameName = "Game1";

            //Act
            var entityExists = this.gamesService.CheckIfEntityExists<Game>(gameName);

            //Assert
            Assert.True(entityExists);
        }

        [Fact]
        public void CheckIfEntityExistsReturnsFalseIfEntityDoesNotExist()
        {
            //Assert
            var gameName = "Game100";

            //Act
            var entityExists = this.gamesService.CheckIfEntityExists<Game>(gameName);

            //Assert
            Assert.False(entityExists);
        }

        [Fact]
        public void GetBiggestIdReturnsCorrectValue()
        {
            //Act
            var biggestId = this.gamesService.GetBiggestId<Game>();

            //Assert
            Assert.Equal(15, biggestId);
        }

        [Fact]
        public void GetBiggestIdReturnsZeroIfNoEntitiesInDbSet()
        {
            //Act
            var biggestId = this.gamesService.GetBiggestId<Developer>();

            //Assert
            Assert.Equal(0, biggestId);
        }

        [Fact]
        public void InputFieldIsNullReturnsTrueIfNull()
        {
            //Assert
            string gameName = null;

            //Act
            var check = this.gamesService.InputFieldIsNull(gameName);

            //Assert
            Assert.True(check);
        }

        [Fact]
        public void InputFieldIsNullReturnsFalseIfNotNull()
        {
            //Assert
            string gameName = "game";

            //Act
            var check = this.gamesService.InputFieldIsNull(gameName);

            //Assert
            Assert.False(check);
        }

        [Fact]
        public void GetMaxPagesReturnsMaxPages()
        {
            var pages = this.gamesService.GetMaxPages();
            Assert.Equal(2, pages);
        }

        [Fact]
        public void GetThreeMostRecentGamesReturnsThreeMostRecentGames()
        {
            
            var recentGames = this.gamesService.GetThreeMostRecentGames();
            Assert.Equal(15, recentGames[0].Id);
            Assert.Equal(14, recentGames[1].Id);
            Assert.Equal(13, recentGames[2].Id);
        }

        [Fact]
        public void GetFourRandomGamesReturnsFourRandomGames()
        {
            var fourRandomGames = this.gamesService.GetFourRandomGames();
            Assert.Equal(4, fourRandomGames.Count);
        }

        [Fact]
        public void GetSingleReturnsCorrectData()
        {
            using var stream = new MemoryStream(File.ReadAllBytes(imgPath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", imgPath.Split(@"\").Last());
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
                GameModeIds = new List<int> { 1, 2 },
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
            var game = this.gamesService.GetSingle(16);
            Assert.Equal("GTA", game.Name);
        }


    }
}
