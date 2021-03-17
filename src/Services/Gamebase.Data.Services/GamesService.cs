namespace Gamebase.Data.Services
{
    using Gamebase.Models;
    using Gamebase.Web.InputModels.AddDelete;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            var game = context.
                Games.
                Where(x => x.Id == id).
                Select(x => new GameOnDetailsPageViewModel
                {
                    Name = x.Name,
                    Summary = x.Summary,
                    Cover = x.Cover.ImageId + ".jpg",
                    Bundles = x.Bundles,
                    Category = x.Category,
                    Collection = x.Collection,
                    Dlcs = x.Dlcs,
                    Expansions = x.Expansions,
                    FirstReleaseDate = x.FirstReleaseDate,
                    AverageRating = $"{Math.Floor((decimal)x.TotalRating)}",
                    GameEngines = x.GameEngines.Select(y => y.GameEngine).ToList(),
                    Developers = x.Developers.Select(y => y.Developer).ToList(),
                    Genres = x.Genres.Select(y => y.Genre).ToList(),
                    GameModes = x.GameModes.Select(y => y.GameMode).ToList(),
                    Keywords = x.Keywords.Select(y => y.Keyword).ToList(),
                    Platforms = x.Platforms.Select(y => y.Platform).ToList(),
                    Screenshots = x.Screenshots.Select(y => y.ImageId + ".jpg").ToList(),
                    SimilarGames = x.SimilarGames,
                    Status = x.Status,
                    Storyline = x.Storyline,
                    Websites = x.Websites,
                }).
                FirstOrDefault();
            if (game != null)
            {
                return game;
            }
            else
            {
                return null;
            }
        }
        public ICollection<Game> GetGamesFromString(string numbers)
        {
            
            if (numbers != null)
            {
                List<int> ids = numbers.Trim().Split(",").Select(int.Parse).ToList();
                
                return null;
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
                    ReleaseDate = x.FirstReleaseDate
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
                    ShortSummary = string.Join(" ", x.Summary.Split(' ', StringSplitOptions.None).ToList().Take(10).ToList()) + "..." ,
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

            if (!CheckIfEntityExists<Developer>(input.DeveloperName))
            {
                context.Add(new Developer
                {
                    Name = input.DeveloperName,
                    Description = input.DeveloperDescription
                });
                context.SaveChanges();
            }

            if (!CheckIfEntityExists<Collection>(input.CollectionName))
            {
                context.Add(new Collection
                {
                    Name = input.CollectionName
                });
                context.SaveChanges();
            }

            if (!CheckIfEntityExists<GameEngine>(input.GameEngineName))
            {
                context.Add(new Collection
                {
                    Name = input.CollectionName
                });
                context.SaveChanges();
            }

            if (!CheckIfEntityExists<GameMode>(input.GameModeName))
            {
                context.Add(new GameMode
                {
                    Name = input.GameModeName
                });
                context.SaveChanges();
            }

            if (!CheckIfEntityExists<Genre>(input.GenreName))
            {
                context.Add(new GameMode
                {
                    Name = input.GameModeName
                });
                context.SaveChanges();
            }

            if (!CheckIfEntityExists<Keyword>(input.KeywordName))
            {
                context.Add(new Keyword
                {
                    Name = input.KeywordName
                });
                context.SaveChanges();
            }

            if (!CheckIfEntityExists<Platform>(input.PlatformName))
            {
                context.Add(new Platform
                {
                    Name = input.PlatformName
                });
                context.SaveChanges();
            }
            if (!CheckIfEntityExists<Character>(input.CharacterName))
            {
                context.Add(new Character
                {
                    Name = input.CharacterName
                });
                context.SaveChanges();
            }

            // no check for screenshot 
            context.Add(new Screenshot
            {
                Url = input.ScreenshotUrl
            });
            context.SaveChanges();


            var Developer= context.Developers.FirstOrDefault(x => x.Name == input.DeveloperName);
            var Collection = context.Collections.FirstOrDefault(x => x.Name == input.CollectionName);
            var GameEnginge= context.GameEngines.FirstOrDefault(x => x.Name == input.GameEngineName);
            var GameMode= context.GameModes.FirstOrDefault(x => x.Name == input.GameModeName);
            var GenreName= context.Genres.FirstOrDefault(x => x.Name == input.GenreName);
            var Keyword= context.Keywords.FirstOrDefault(x => x.Name == input.KeywordName);
            var Platform= context.Platforms.FirstOrDefault(x => x.Name == input.PlatformName);
            var Character = context.Characters.FirstOrDefault(x => x.Name == input.CharacterName);
            var Screenshot= context.Screenshots.FirstOrDefault(x => x.Url == input.ScreenshotUrl);
            if (!CheckIfEntityExists<Game>(input.Name))
            {
                var newGame = new Game();
                newGame.Name = input.Name;
                //newGame.Cover = input.Cover; IMAGE
                newGame.Storyline = input.Storyline;
                newGame.Summary = input.Summary;
                //newGame.Developers
                newGame.Collection.Id = Collection.Id;
                //newGame.GameEngines 
                //newGame.GameMode 
                //newGame.Keywords
                //newGame.Platforms
                //newGame.Character
                newGame.Screenshots.Add(Screenshot);  //IMAGE
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
