﻿namespace Gamebase.Data.Services
{
    using Gamebase.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.ViewModels.Games;

    public class GamesService : IGamesService
    {
        private readonly GamebaseDbContext context;

        public GamesService(GamebaseDbContext context)
        {
            this.context = context;
        }

        public List<GameOnAllPageViewModel> GetAll(int currentPage)
        {
            const int gamesOnPage = 10;
            var pageCount = Math.Ceiling((decimal)context.Games.Count() / gamesOnPage);
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
                    Cover = x.Cover,
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
                    Screenshots = x.Screenshots,
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
        public  ICollection<Game> GetGamesFromString(string numbers){
            if (numbers != null) {
                List<int> ids = numbers.Trim().Split(",").Select(int.Parse).ToList();
                return null; 
            }
            else
            {
                return null;
            }
        }
    }
}
