namespace Gamebase.Data.Services
{
    using Gamebase.Models;
    using Gamebase.Web.InputModels.AddDelete;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.ViewModels.Games;
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
                    Screenshots = x.Screenshots.Select(x => x.ImageId + ".jpg").ToList(),
                    SimilarGames = x.SimilarGames,
                    Status = x.Status,
                    Storyline = x.Storyline,
                    Websites = x.Websites,
                    Characters = x.Characters.Select(y => y.Character).ToList(),
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

        public ICollection<SearchGameViewModel> GetGameByName(string name, string developerName)
        {
            var games = context
                .Games
                .Select(x => new SearchGameViewModel
                {
                    Name = x.Name,
                    DeveloperName = x.Developers.Count >= 1 ? x.Developers.Select(d => d.Developer.Name).FirstOrDefault() : null,
                    ReleaseDate = x.FirstReleaseDate
                })
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .ToList();

            if (developerName != null)
            {
                return games.Where(x => x.DeveloperName.ToLower().Contains(developerName.ToLower())).ToList();
            }

            return games;
        }

        public void AddGame(AddGameInputModel input)
        {
            var existingGame = context.Games.FirstOrDefault(x => x.Name == input.Name);
            var existingDeveloper = context.Developers.FirstOrDefault(x => x.Name == input.DeveloperName);
            if (existingDeveloper == null)
                {
                context.Add(new Developer { 
                Name=input.DeveloperName,
                Url=input.DeveloperUrl,
                Description=input.DeveloperDescription
                });
                context.SaveChanges();
                existingDeveloper = context.Developers.FirstOrDefault(x => x.Name == input.DeveloperName);
            }
            if (existingGame == null)
            {
                context.Add(new Game
                {
                    Name = input.Name,
                    Url = input.Url,
                    Storyline = input.Storyline,
                    Summary = input.Summary,
                    Status = input.Status,
                    Category = input.Category,
                    FirstReleaseDate = input.FirstReleaseDate,
                    Developers=null,
                    

                }); ;
                existingGame = context.Games.FirstOrDefault(x => x.Name == input.Name);
                //existingGame.Developers.Add(existingDeveloper.Id);
                context.SaveChanges();
                    
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

        public decimal GetMaxPages() => Math.Ceiling((decimal)context.Games.Count() / gamesOnPage);
    }
}
