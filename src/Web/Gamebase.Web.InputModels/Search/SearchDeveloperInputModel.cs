namespace Gamebase.Web.InputModels.Search
{
    using System.ComponentModel.DataAnnotations;

    public class SearchDeveloperInputModel
    {
        [Required]
        [Display(Name = "Developer name")]
        public string Name { get; set; }
    }
}
