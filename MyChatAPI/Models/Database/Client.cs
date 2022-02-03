using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Models.Database
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Chat> Chats { get; set; }
    }
}
