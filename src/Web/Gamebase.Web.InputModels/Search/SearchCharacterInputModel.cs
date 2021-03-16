namespace Gamebase.Web.InputModels.Search
{
    using System.ComponentModel.DataAnnotations;

    public class SearchCharacterInputModel
    {
        [Required]
        [Display(Name = "Character name")]
        public string Name { get; set; }
    }
}
