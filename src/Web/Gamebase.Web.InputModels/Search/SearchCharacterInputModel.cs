namespace Gamebase.Web.InputModels.Search
{
    using System.ComponentModel.DataAnnotations;

    public class SearchCharacterInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Character name")]
        public string Name { get; set; }
    }
}
