namespace Gamebase.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using Games;

    public class UserAccountViewModel
    {
        public string Username { get; set; }

        public List<GameOnAllPageViewModel> GamesByUser { get; set; }
    }
}
