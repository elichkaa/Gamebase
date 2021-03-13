using Gamebase.Models;
using Gamebase.Web.ViewModels.Developers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Data.Services.Contracts
{
    public interface IDeveloperService
    {
        public DeveloperOnSinglePageViewModel GetDeveloper(string name);
    }
}
