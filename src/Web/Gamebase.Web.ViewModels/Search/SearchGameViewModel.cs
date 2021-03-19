namespace Gamebase.Web.ViewModels.Search
{
    using System;

    public class SearchGameViewModel
    {
        public int Id { get; set; }
     
        public string Name { get; set; }

        public string DeveloperName { get; set; }
        
        public string ReleaseDate { get; set; }

        public string Cover { get; set; }

        public bool IsFromUser { get; set; }
    }
}
