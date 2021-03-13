using Gamebase.Models;
using Gamebase.Web.ViewModels.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamebase.Web.ViewModels.Developers;

namespace Gamebase.Data.Services.Contracts
{
    public class DeveloperService : IDeveloperService
    {
        private readonly GamebaseDbContext context;

        public DeveloperService(GamebaseDbContext context)
        {
            this.context = context;
        }
        public DeveloperOnSinglePageViewModel GetDeveloper(string name)
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
    }
}
