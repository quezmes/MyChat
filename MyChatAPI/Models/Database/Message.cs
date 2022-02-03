using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Models.Database
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}