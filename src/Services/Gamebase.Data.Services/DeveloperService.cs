using Gamebase.Models;
using Gamebase.Web.ViewModels.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Data.Services.Contracts
{
    public class DeveloperService : IDeveloperService
    {
        private readonly GamebaseDbContext context;

        public DeveloperService(GamebaseDbContext context)
        {
            this.context = context;
        }
        public Developer GetDeveloper(string name)
        {
            var developer = context.
                 Developers.
                 Where(x => x.Name == name).
                 Select(x => new GameOnDetailsPageViewModel());
            return null;
        }

    }
}
