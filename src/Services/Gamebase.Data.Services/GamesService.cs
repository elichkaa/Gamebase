namespace Gamebase.Data.Services
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

        public List<GameOnAllPageViewModel> GetAll()
        {
            var games = context
                .Games
                .OrderByDescending(x => x.FirstReleaseDate)
                .Select(x => new GameOnAllPageViewModel
                {
                    Name = x.Name,
                    Summary = x.Summary,
                    Cover = x.Cover.ImageId + ".jpg",
                    DeveloperName = x.Developers.Select(d => d.Developer.Name).FirstOrDefault(),
                    AverageRating = $"{x.TotalRating:f2}"
                })
                .ToList();
            return games;
        }

        public GameOnDetailsPageViewModel GetSingle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
