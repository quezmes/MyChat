using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MyChatAPI.SingalR
{
    public class PresenceHub :Hub
    {
        [Authorize]
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("UserIsOnline", Context.User);
        }
    }
}
