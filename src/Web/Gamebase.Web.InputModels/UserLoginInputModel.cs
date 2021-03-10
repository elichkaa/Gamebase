namespace Gamebase.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 6)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
