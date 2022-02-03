using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Models.Database
{
    public class Chat
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ApiKey { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<Message> Messages;
    }
}