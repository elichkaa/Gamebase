using System;
using System.Collections.Generic;
using Gamebase.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Web.ViewModels.Developers
{
    public class DeveloperOnSinglePageViewModel
    {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Description { get; set; }
            public ICollection<Game> Games { get; set; }
            public string PublishedGames { get; set; }
            public int ParentCompanyId { get; set; }
        }
    }



