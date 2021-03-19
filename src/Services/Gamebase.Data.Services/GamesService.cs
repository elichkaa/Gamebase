namespace Gamebase.Data.Services
{
    using Models;
    using Web.InputModels.AddDelete;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Models.Enums;
    using Web.InputModels.Search;
    using Web.ViewModels.Games;
    using Web.ViewModels.Home;
    using Web.ViewModels.Search;
    using Collection = Models.Collection;

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
            return this.context
                .Games
                .OrderByDescending(x => x.FirstReleaseDate)
                .Select(x => new GameOnHomePageViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Cover = x.Cover != null ? x.Cover.ImageId + ".jpg" : null,
                    ShortSummary = x.Summary != null ? string.Join(" ", x.Summary.Split(' ', StringSplitOptions.None).ToList().Take(10).ToList()) + "..." : null,
                    MainGenreName = x.Genres.Count != 0 ? x.Genres.Select(g => g.Genre.Name).FirstOrDefault() : null
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
                    Cover = x.Cover != null ? x.Cover.ImageId + ".jpg" : null,
                    ShortSummary = x.Summary != null ? string.Join(" ", x.Summary.Split(' ', StringSplitOptions.None).ToList().Take(10).ToList()) + "..." : null,
                    MainGenreName = x.Genres.Count != 0 ? x.Genres.Select(g => g.Genre.Name).FirstOrDefault() : null
                })
                .Take(4)
                .ToList();
        }

        public ICollection<GameMode> GetGameModes()
        {
            return this.context
                .GameModes
                .AsNoTracking()
                .ToList();
        }

        public void AddGame(AddGameInputModel input, string userId, string basePath)
        {
            //Check if entities exist, if they dont add them
            var developerNames = new List<string>();
            if (!this.InputFieldIsNull(input.DeveloperNames))
            {
                developerNames = input.DeveloperNames.Split(",").ToList();
                foreach (string developerName in developerNames)
                {
                    if (!CheckIfEntityExists<Developer>(developerName))
                    {
                        context.Developers.Add(new Developer
                        {
                            Id = GetBiggestId<Developer>() + 1,
                            Name = developerName
                        });
                        context.SaveChanges();
                    }
                }
            }

            if (input.CollectionName != null)
            {
                if (!CheckIfEntityExists<Collection>(input.CollectionName))
                {
                    context.Collections.Add(new Collection
                    {
                        Id = GetBiggestId<Collection>() + 1,
                        Name = input.CollectionName
                    });
                    context.SaveChanges();
                }
            }

            var gameEngineNames = new List<string>();
            if (!this.InputFieldIsNull(input.GameEngineNames))
            {
                gameEngineNames = input.GameEngineNames.Split(",").ToList();
                foreach (string gameEngineName in gameEngineNames)
                {
                    if (!CheckIfEntityExists<GameEngine>(gameEngineName))
                    {
                        context.GameEngines.Add(new GameEngine
                        {
                            Id = GetBiggestId<GameEngine>() + 1,
                            Name =gameEngineName
                        });
                        context.SaveChanges();
                    }
                }
            }

            var genreNames = new List<string>();
            if (!this.InputFieldIsNull(input.GenreNames))
            {
                genreNames = input.GenreNames.Split(",").ToList();
                foreach (string genreName in genreNames)
                {
                    if (!CheckIfEntityExists<Genre>(genreName))
                    {
                        context.Genres.Add(new Genre
                        {
                            Id = GetBiggestId<Genre>() + 1,
                            Name = genreName
                        });
                        context.SaveChanges();
                    }
                }
            }

            var keywordNames = new List<string>();
            if (!this.InputFieldIsNull(input.KeywordNames))
            {
                keywordNames = input.KeywordNames.Split(",").ToList();
                foreach (string keyword in keywordNames)
                {
                    if (!CheckIfEntityExists<Keyword>(keyword))
                    {
                        context.Add(new Keyword
                        {
                            Id = GetBiggestId<Keyword>() + 1,
                            Name = keyword
                        });
                        context.SaveChanges();
                    }
                }
            }

            var platformNames = new List<string>();
            if (!this.InputFieldIsNull(input.PlatformNames))
            {
                platformNames = input.PlatformNames.Split(",").ToList();
                foreach (string platformName in platformNames)
                {
                    if (!CheckIfEntityExists<Platform>(platformName))
                    {
                        context.Add(new Platform
                        {
                            Id = GetBiggestId<Platform>() + 1,
                            Name = platformName
                        });
                        context.SaveChanges();
                    }
                }
            }

            var characterNames = new List<string>();
            if (!this.InputFieldIsNull(input.CharacterNames))
            {
                characterNames = input.CharacterNames.Split(",").ToList();
                foreach (string characterName in characterNames)
                {
                    if (!CheckIfEntityExists<Character>(characterName))
                    {
                        context.Add(new Character
                        {
                            Id = GetBiggestId<Character>() + 1,
                            Name = characterName
                        });
                        context.SaveChanges();
                    }
                }
            }

            var collection = context.Collections.FirstOrDefault(x => x.Name == input.CollectionName);
            var developers = context.Developers.Where(x => developerNames.Contains(x.Name)).ToList();
            var gameEngines = context.GameEngines.Where(x => gameEngineNames.Contains(x.Name)).ToList();
            var genres = context.Genres.Where(x => genreNames.Contains(x.Name)).ToList();
            var keywords = context.Keywords.Where(x => keywordNames.Contains(x.Name)).ToList();
            var platforms = context.Platforms.Where(x => platformNames.Contains(x.Name)).ToList();
            var characters = context.Characters.Where(x => characterNames.Contains(x.Name)).ToList();

            if (CheckIfEntityExists<Game>(input.Name))
            {
                return;
            }

            var gameId = this.GetBiggestId<Game>() + 1;
            var newGame = new Game
            {
                Id = gameId,
                Name = input.Name,
                Category = GameCategoryEnum.main_game,
                Collection = collection,
                FirstReleaseDate = input.FirstReleaseDate,
                Status = StatusEnum.released,
                Storyline = input.Storyline,
                Summary = input.Summary,
            };
            newGame.GameModes.Add(new GamesGameModes()
            {
                GameId = gameId,
                GameModeId = input.GameModeId
            });

            Directory.CreateDirectory(basePath);
            if (input.Cover != null)
            {
                newGame.Cover = new Cover
                {
                    Id = this.GetBiggestId<Cover>() + 1,
                    ImageId = input.Cover.FileName,
                    Url = $"{basePath}{input.Cover.FileName}",
                    GameId = newGame.Id,
                    ApplicationUserId = userId,
                };
            }
            for (int i = 0; i < input.Screenshots.Count; i++)
            {
                var screenshot = input.Screenshots.ToList()[i];
                var dbScreenshot = new Screenshot
                {
                    Id = this.GetBiggestId<Screenshot>() + i + 1,
                    Url = $"{basePath}{screenshot.FileName}",
                    ApplicationUserId = userId,
                    GameId = newGame.Id,
                    ImageId = screenshot.FileName
                };
                newGame.Screenshots.Add(dbScreenshot);

                using Stream fileStream = new FileStream(dbScreenshot.Url, FileMode.Create);
                screenshot.CopyTo(fileStream);
            }

            context.Games.Add(newGame);
            context.SaveChanges();

            newGame = context.Games.FirstOrDefault(x => x.Name == input.Name);
            foreach (Developer developer in developers)
            {
                context.GamesDevelopers.Add(new GamesDevelopers(newGame.Id, developer.Id));
            }
            foreach (GameEngine gameEngine in gameEngines)
            {
                context.GamesEngines.Add(new GamesGameEngines(newGame.Id, gameEngine.Id));
            }
            foreach (Genre genre in genres)
            {
                context.GameGenres.Add(new GamesGenres(newGame.Id, genre.Id));
            }
            foreach (Keyword keyword in keywords)
            {
                context.GamesKeywords.Add(new GamesKeywords(newGame.Id, keyword.Id));
            }
            foreach (Platform platform in platforms)
            {
                context.GamePlatforms.Add(new GamesPlatforms(newGame.Id, platform.Id));
            }
            foreach (Character character in characters)
            {
                context.GameCharacters.Add(new GamesCharacters(newGame.Id, character.Id));
            }

            context.SaveChanges();
        }

        public void DeleteGame(DeleteGameInputModel input)
        {
            var existingGame = context.Games.FirstOrDefault(x => x.Name == input.Name);
            if (existingGame != null)
            {
                context.Games.Remove(existingGame);
                context.SaveChanges();
            }
        }

        private bool CheckIfEntityExists<T>(string name) where T : BaseEntity
        {
            return this.context.Set<T>().Any(x => x.Name.ToLower() == name.ToLower());
        }

        private int GetBiggestId<T>() where T : MainEntity
        {
            return this.context.Set<T>().AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefault().Id;
        }

        private bool InputFieldIsNull(string field)
        {
            return field == null;
        }
    }
}
