using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        public String Email { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public String ConfirmPassword { get; set; }
    }
}
