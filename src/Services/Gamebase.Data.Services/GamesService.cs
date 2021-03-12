﻿namespace Gamebase.Data.Services
{
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
                    AverageRating = $"{x.TotalRating:f2}",
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
            var game=context.
                Games.
                Where(x=>x.Id==id).
                Select(x=>new GameOnDetailsPageViewModel
                {
                    Developers=x.Developers.Select(y=>y.Developer).ToList(),
                    Genres=x.Genres.Select(y=>y.Genre.Name).ToList()
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
    }
}
