using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {//adding validation with data annotation
        [Required]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "You must specify username with between 2 and 16 characters.")]
        public string Username { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password with between 4 and 8 characters.")]
        public string Password { get; set; }
    }
}