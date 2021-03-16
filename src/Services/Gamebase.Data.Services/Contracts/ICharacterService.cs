using Gamebase.Web.ViewModels.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Data.Services.Contracts
{
    public interface ICharacterService
    {
        public List<CharacterOnAllPageViewModel> GetAll(int currentPage);
    }
}
