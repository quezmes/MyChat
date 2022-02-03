using MyChatAPI.Context;
using MyChatAPI.Models.Database;

namespace MyChatAPI.Services
{

    public interface IChatService
    {
        Chat? GetChatByChatKey(string chatKey);
        ICollection<Message>? GetMessagesByChatKey(string chatKey, int count);
        ICollection<Chat>? GetChatsByUsername(string username);
        Task<bool> AddMessageToChat(Message message, string chatKey);

        Task<bool> CreateChat(string chatName, Client client);
    }
    public class ChatService : IChatService
    {
        private readonly ClientDbContext _context;

        public ChatService(ClientDbContext context)
        {
            _context = context;
        }


        public async Task<bool> AddMessageToChat(Message message, string chatKey)
        {
            var chat = GetChatByChatKey(chatKey);
            if (chat == null) return false;

            try
            {
                chat.Messages.Add(message);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateChat(string chatName, Client client)
        {
            client.Chats.Add(new Chat { IsActive = true, Name = chatName });
            return await _context.SaveChangesAsync() > 0;
        }

        public Chat? GetChatByChatKey(string chatKey)
          => _context.Chats.FirstOrDefault(x => x.ApiKey == chatKey);

        public Chat? GetChatByName(string chatName)
        {
            throw new NotImplementedException();
        }

        public ICollection<Chat>? GetChatsByUsername(string username)
        => _context.Clients.FirstOrDefault(c => c.UserName == username)?.Chats;

        public ICollection<Message>? GetMessagesByChatKey(string chatKey, int count)
       => GetChatByChatKey(chatKey)?.Messages.OrderBy(m => m.Date).TakeLast(count).ToList();
    }
}
