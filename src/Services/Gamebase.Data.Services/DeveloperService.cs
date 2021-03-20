namespace Gamebase.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Web.InputModels.Search;
    using Web.ViewModels.Developers;
    using Web.ViewModels.Search;

    public class DeveloperService : IDeveloperService
    {
        private readonly GamebaseDbContext context;

        public DeveloperService(GamebaseDbContext context)
        {
            this.context = context;
        }
        public DeveloperOnSinglePageViewModel GetSingleDeveloper(string name)
        {
            var developer = context.
                 Developers.
                 Where(x => x.Name == name).
                 Select(x => new DeveloperOnSinglePageViewModel { 
                 Name=x.Name,
                 Url=x.Url,
                 Description=x.Description,
                 Games = x.Games.Select(y => y.Game).ToList(),
                 PublishedGames=x.PublishedGames,
                 ParentCompanyId=x.ParentCompanyId
                 }).
                 FirstOrDefault();

            if (developer != null)
            {
                return developer;
            }
            else
            {
                return null;
            }
        }

        public ICollection<SearchDeveloperViewModel> GetDeveloperByName(SearchDeveloperInputModel input)
        {
            var developers = context
                .Developers
                .Where(x => x.Name.ToLower().Contains(input.Name.ToLower()))
                .Select(x => new SearchDeveloperViewModel
                {
                    Name = x.Name,
                    GamesByDeveloper = x.Games.Select(g => new SearchGameViewModel
                    {
                        Id = g.GameId,
                        Name = g.Game.Name,
                        Cover = g.Game.ApplicationUserId == null ? g.Game.Cover.ImageId + ".jpg" : g.Game.Cover.ImageId,
                        IsFromUser = g.Game.ApplicationUserId != null
                    }).ToList()
                })
                .ToList();

            return developers;
        }
    }
}
