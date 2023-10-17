using System.ComponentModel.DataAnnotations;

namespace AdminPageMVC.DTO
{
    public class UserDTO
    {

        [Required(ErrorMessage = "User full anme required")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
