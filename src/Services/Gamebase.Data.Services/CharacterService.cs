using Gamebase.Data.Services.Contracts;
using Gamebase.Web.ViewModels.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Data.Services
{
    class CharacterService : ICharacterService
    {
        private readonly GamebaseDbContext context;
        private const int charactersOnPage = 10;

        public CharacterService(GamebaseDbContext context)
        {
            this.context = context;
        }
        public decimal GetMaxPages() => Math.Ceiling((decimal)context.Characters.Count() / charactersOnPage);
        public List<CharacterOnAllPageViewModel> GetAll(int currentPage)
        {

                var pageCount = this.GetMaxPages();
                var characters = context
                    .Characters
                    .OrderByDescending(x => x.ImageId)
                    .Select(x => new CharacterOnAllPageViewModel
                    {
                        Name = x.Name,
                        Image = x.Image.ImageId + ".jpg",
                        PageCount = (int)pageCount,
                        CurrentPage = currentPage
                    })
                    .Skip((currentPage - 1) * charactersOnPage)
                    .Take(charactersOnPage)
                    .ToList();
            return characters;
        }
        
    }
}
