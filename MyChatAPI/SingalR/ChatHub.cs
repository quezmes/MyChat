using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MyChatAPI.Services
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContxt = Context.GetHttpContext();
            var chatKey = httpContxt.Request.Query["chatKey"].ToString();
            var chat = _chatService.GetChatByChatKey(chatKey);
            if (chat == null || !chat.IsActive) throw new HubException("Chat douesn't exist or was deactivated!");
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatKey);


                var messages = _chatService.GetMessagesByChatKey(chatKey, 100);
                await Clients.Group(chatKey).SendAsync("GetMessages", messages);
            }
            //return base.OnConnectedAsync();
        }

        //public async Task SendMessage(Message)

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
