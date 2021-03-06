﻿namespace Gamebase.Web.ViewModels.Games
{
    using Gamebase.Models;

    public class GameOnAllPageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Cover { get; set; }

        public string AverageRating { get; set; }

        public int PageCount { get; set; }

        public int CurrentPage { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
