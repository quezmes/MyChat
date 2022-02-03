using Microsoft.EntityFrameworkCore;
using MyChatAPI.Models.Database;

namespace MyChatAPI.Context
{
    public class ClientDbContext : DbContext
    {

        public DbSet<Client> Clients{ get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ClientDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
