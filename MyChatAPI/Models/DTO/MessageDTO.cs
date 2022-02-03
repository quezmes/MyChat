using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Models.DTO
{
    public class MessageDTO
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
