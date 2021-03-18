namespace Gamebase.Web.InputModels.Search
{
    using System.ComponentModel.DataAnnotations;

    public class SearchGameInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Game name")]
        public string Name { get; set; }

        [Display(Name = "Developer Name")]
        public string DeveloperName { get; set; }
    }
}
