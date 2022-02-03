using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public string EmailOrUserName{ get; set; }
        [Required]
        public string Password { get; set; }
    }
}
