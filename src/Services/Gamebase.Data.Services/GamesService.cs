namespace Gamebase.Data.Services
{
    using Gamebase.Models;
    using Gamebase.Web.InputModels.AddDelete;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Web.InputModels.Search;
    using Web.ViewModels.Games;
    using Web.ViewModels.Home;
    using Web.ViewModels.Search;

    public class GamesService : IGamesService
    {
        private readonly GamebaseDbContext context;
        private const int gamesOnPage = 10;

        public GamesService(GamebaseDbContext context)
        {
            this.context = context;
        }

        public List<GameOnAllPageViewModel> GetAll(int currentPage)
        {
            var pageCount = this.GetMaxPages();
            if (currentPage <= 0 || currentPage > pageCount)
            {
                return null;
            }
            var games = context
                .Games
                .OrderByDescending(x => x.FirstReleaseDate)
                .Select(x => new GameOnAllPageViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Summary = x.Summary,
                    Cover = x.Cover.ImageId + ".jpg",
                    AverageRating = $"{Math.Floor((decimal)x.TotalRating)}",
                    PageCount = (int)pageCount,
                    CurrentPage = currentPage
                })
                .Skip((currentPage - 1) * gamesOnPage)
                .Take(gamesOnPage)
                .ToList();

            return games;
        }

        public GameOnDetailsPageViewModel GetSingle(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var game = context.
                Games.
                AsNoTracking().
                Where(x => x.Id == id).
                Select(x => new GameOnDetailsPageViewModel
                {
                    Name = x.Name,
                    Summary = x.Summary,
                    Cover = x.Cover.ImageId + ".jpg",
                    FirstReleaseDate = x.FirstReleaseDate,
                    AverageRating = $"{Math.Floor((decimal)x.TotalRating)}",
                    GameEngines = x.GameEngines.Select(y => y.GameEngine.Name).ToList(),
                    Developers = x.Developers.Select(y => y.Developer.Name).ToList(),
                    Genres = x.Genres.Select(y => y.Genre).ToList(),
                    Keywords = x.Keywords.Select(y => y.Keyword.Name).ToList(),
                    Platforms = x.Platforms.Select(y => y.Platform.Name).ToList(),
                    Screenshots = x.Screenshots.Select(y => y.ImageId + ".jpg").ToList(),
                    Status = x.Status,
                    Storyline = x.Storyline,
                }).
                FirstOrDefault();
            return game;
        }
        public ICollection<Game> GetGamesFromString(string numbers)
        {
            if (numbers != null)
            {
                List<int> ids = numbers.Trim().Split(", ").Select(int.Parse).ToList();

                return context.Games.Where(x => ids.Contains(x.Id)).ToList();
            }
            else
            {
                return null;
            }
        }
        public ICollection<SearchGameViewModel> GetGame(SearchGameInputModel input)
        {
            var games = context
                .Games
                .Where(x => x.Name.ToLower().Contains(input.Name.ToLower()))
                .Select(x => new SearchGameViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    DeveloperName = x.Developers.Count >= 1 ? x.Developers.Select(d => d.Developer.Name).FirstOrDefault() : null,
                    ReleaseDate = ((DateTime)x.FirstReleaseDate).ToString("MM/dd/yyyy")
                })
                .ToList();

            if (input.DeveloperName != null)
            {
                return games.Where(x => x.DeveloperName.ToLower().Contains(input.DeveloperName.ToLower())).ToList();
            }

            return games;
        }

        public decimal GetMaxPages() => Math.Ceiling((decimal)context.Games.Count() / gamesOnPage);

        public List<GameOnHomePageViewModel> GetThreeMostRecentGames()
        {
            return context
                .Games
                .OrderByDescending(x => x.FirstReleaseDate)
                .Select(x => new GameOnHomePageViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Cover = x.Cover.ImageId + ".jpg",
                    ShortSummary = string.Join(" ", x.Summary.Split(' ', StringSplitOptions.None).ToList().Take(10).ToList()) + "...",
                    MainGenreName = x.Genres.Select(g => g.Genre.Name).FirstOrDefault()
                })
                .Take(3)
                .ToList();
        }

        public List<GameOnHomePageViewModel> GetFourRandomGames()
        {
            return context
                .Games
                .OrderBy(x => Guid.NewGuid())
                .Select(x => new GameOnHomePageViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Cover = x.Cover.ImageId + ".jpg",
                    ShortSummary = string.Join(" ", x.Summary.Split(' ', StringSplitOptions.None).ToList().Take(10).ToList()) + "...",
                    MainGenreName = x.Genres.Select(g => g.Genre.Name).FirstOrDefault()
                })
                .Take(4)
                .ToList();
        }

        public void AddGame(AddGameInputModel input)
        {
            //Check if entities exist, if they dont add them

            List<string> developerNames = input.DeveloperName.Split(" ").ToList();
            foreach (string developerName in developerNames)
            {
                if (!CheckIfEntityExists<Developer>(developerName))
                {
                    context.Add(new Developer
                    {
                        Name = developerName
                    });
                    context.SaveChanges();
                }
            }
            if (!CheckIfEntityExists<Collection>(input.CollectionName))
            {
                context.Add(new Collection
                {
                    Name = input.CollectionName
                });
                context.SaveChanges();
            }

            List<string> gameEngineNames = input.GameEngineName.Split(" ").ToList();
            foreach (string gameEngineName in gameEngineNames)
            {
                if (!CheckIfEntityExists<GameEngine>(gameEngineName))
                {
                    context.Add(new Collection
                    {
                        Name = input.CollectionName
                    });
                    context.SaveChanges();
                }
            }
            List<string> gameModeNames = input.GameModeName.Split(" ").ToList();
            foreach (string gameModeName in gameModeNames)
            {
                if (!CheckIfEntityExists<GameMode>(gameModeName))
                {
                    context.Add(new GameMode
                    {
                        Name = gameModeName
                    });
                    context.SaveChanges();
                }
            }

            List<string> genreNames = input.GenreName.Split(" ").ToList();
            foreach (string genreName in genreNames)
            {
                if (!CheckIfEntityExists<Genre>(genreName))
                {
                    context.Add(new GameMode
                    {
                        Name = genreName
                    });
                    context.SaveChanges();
                }
            }

            List<string> keywordNames = input.KeywordName.Split(" ").ToList();
            foreach (string keyword in keywordNames)
            {
                if (!CheckIfEntityExists<Keyword>(keyword))
                {
                    context.Add(new Keyword
                    {
                        Name = keyword
                    });
                    context.SaveChanges();
                }
            }
            List<string> platformNames = input.PlatformName.Split(" ").ToList();
            foreach (string platformName in platformNames)
            {
                if (!CheckIfEntityExists<Platform>(platformName))
                {
                    context.Add(new Platform
                    {
                        Name = platformName
                    });
                    context.SaveChanges();
                }
            }

            List<string> characterNames = input.CharacterName.Split(" ").ToList();
            foreach (string characterName in characterNames)
            {
                if (!CheckIfEntityExists<Character>(characterName))
                {
                    context.Add(new Character
                    {
                        Name = characterName
                    });
                    context.SaveChanges();
                }
            }
            List<string> screenshotsUrls = input.ScreenshotUrl.Split(" ").ToList();
            foreach (string screenshotUrl in screenshotsUrls)
            {
                context.Add(new Screenshot
                {
                    Url = screenshotUrl
                });
                context.SaveChanges();
            }

            //var Developer = context.Developers.FirstOrDefault(x => x.Name == input.DeveloperName);
            var collection = context.Collections.FirstOrDefault(x => x.Name == input.CollectionName);
            //var GameEnginge = context.GameEngines.FirstOrDefault(x => x.Name == input.GameEngineName);
            //var GameMode = context.GameModes.FirstOrDefault(x => x.Name == input.GameModeName);
            //var GenreName = context.Genres.FirstOrDefault(x => x.Name == input.GenreName);
            //var Keyword = context.Keywords.FirstOrDefault(x => x.Name == input.KeywordName);
            //var Platform = context.Platforms.FirstOrDefault(x => x.Name == input.PlatformName);
            //var Character = context.Characters.FirstOrDefault(x => x.Name == input.CharacterName);
            //var Screenshot = context.Screenshots.FirstOrDefault(x => x.Url == input.ScreenshotUrl);
            List<Developer> developers = context.Developers.Where(x => developerNames.Contains(x.Name)).ToList();
            List<GameEngine> gameEngines = context.GameEngines.Where(x => gameEngineNames.Contains(x.Name)).ToList();
            List<GameMode> gameModes = context.GameModes.Where(x => gameModeNames.Contains(x.Name)).ToList();
            List<Genre> genres = context.Genres.Where(x => genreNames.Contains(x.Name)).ToList();
            List<Keyword> keywords = context.Keywords.Where(x => keywordNames.Contains(x.Name)).ToList();
            List<Platform> platforms = context.Platforms.Where(x => platformNames.Contains(x.Name)).ToList();
            List<Character> characters = context.Characters.Where(x => characterNames.Contains(x.Name)).ToList();
            List<Screenshot> screenshots = context.Screenshots.Where(x => screenshotsUrls.Contains(x.Url)).ToList();
            var newGame = new Game();

            if (!CheckIfEntityExists<Game>(input.Name))
            {

                newGame.Name = input.Name;
                //newGame.Cover = input.Cover; IMAGE
                newGame.Storyline = input.Storyline;
                newGame.Summary = input.Summary;
                newGame.Collection = collection;
                foreach (Screenshot screenshot in screenshots)
                {
                    newGame.Screenshots.Add(screenshot);
                }
                context.Games.Add(newGame);
                context.SaveChanges();
            }

            newGame = context.Games.FirstOrDefault(x => x.Name == input.Name);
            foreach (Developer developer in developers)
            {
                context.GamesDevelopers.Add(new GamesDevelopers(newGame, developer));
            }
            foreach (GameEngine gameEngine in gameEngines)
            {
                context.GamesEngines.Add(new GamesGameEngines(newGame, gameEngine));
            }
            foreach (GameMode gameMode in gameModes)
            {
                context.GamesModes.Add(new GamesGameModes(newGame, gameMode));
            }
            foreach (Genre genre in genres)
            {
                context.GameGenres.Add(new GamesGenres(newGame, genre));
            }
            foreach (Keyword keyword in keywords)
            {
                context.GamesKeywords.Add(new GamesKeywords(newGame, keyword));
            }
            foreach (Platform platform in platforms)
            {
                context.GamePlatforms.Add(new GamesPlatforms(newGame, platform));
            }
            foreach (Character character in characters)
            {
                context.GameCharacters.Add(new GamesCharacters(newGame, character));
            }
        }

        public void DeleteGame(DeleteGameInputModel input)
        {
            var existingGame = context.Games.FirstOrDefault(x => x.Name == input.Name);
            if (existingGame != null)
            {
                context.Games.Remove(existingGame);
                context.SaveChanges();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private bool CheckIfEntityExists<T>(string name) where T : BaseEntity
        {
            return this.context.Set<T>().Any(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
