﻿using Gamebase.Data.Services.Contracts;
using Gamebase.Web.ViewModels.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Data.Services
{
    using Web.InputModels.Search;
    using Web.ViewModels.Search;

    public class CharacterService : ICharacterService
    {
        private readonly GamebaseDbContext context;
        private const int charactersOnPage = 9;

        public CharacterService(GamebaseDbContext context)
        {
            this.context = context;
        }
        public decimal GetMaxPages() => Math.Ceiling((decimal)context.Characters.Count() / charactersOnPage);

        public List<CharacterOnAllPageViewModel> GetAll(int currentPage)
        {
            var pageCount = this.GetMaxPages();
            if (currentPage <= 0 || currentPage > pageCount)
            {
                return null;
            }
            var characters = context
                .Characters
                .OrderByDescending(x => x.Image)
                .ThenByDescending(x => x.ImageId)
                .Select(x => new CharacterOnAllPageViewModel
                {
                    Name = x.Name,
                    Image = x.Image.ImageId + ".jpg",
                    PageCount = (int)pageCount,
                    CurrentPage = currentPage,
                    GameNames = string.Join(", ", x.Games.Select(g => g.Game.Name).ToList())
                })
                .Skip((currentPage - 1) * charactersOnPage)
                .Take(charactersOnPage)
                .ToList();
            return characters;
        }

        public ICollection<SearchCharacterViewModel> GetCharacterByName(SearchCharacterInputModel input)
        {

            var characters = context
                .Characters
                .Where(x => x.Name.ToLower().Contains(input.Name.ToLower()))
                .Select(x => new SearchCharacterViewModel
                {
                    Name = x.Name,
                    Image = x.Image.ImageId + ".jpg",
                    Games = x.Games.Select(g => new SearchGameViewModel
                    {
                        Id = g.Game.Id,
                        Name = g.Game.Name,
                        Cover = g.Game.ApplicationUserId == null ? g.Game.Cover.ImageId + ".jpg" : g.Game.Cover.ImageId,
                        IsFromUser = g.Game.ApplicationUserId != null
                    }).ToList()
                })
                .ToList();

            return characters;
        }
    }
}
