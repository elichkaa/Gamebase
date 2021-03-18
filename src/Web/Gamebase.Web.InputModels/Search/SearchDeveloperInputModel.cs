namespace Gamebase.Web.InputModels.Search
{
    using System.ComponentModel.DataAnnotations;

    public class SearchDeveloperInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Developer name")]
        public string Name { get; set; }
    }
}
