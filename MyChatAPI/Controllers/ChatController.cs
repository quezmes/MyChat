using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyChatAPI.Extensions;
using MyChatAPI.Models.Database;
using MyChatAPI.Services;
using System.Security.Claims;

namespace MyChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IClientService _clientService;

        public ChatController(IChatService chatService, IClientService clientService = null)
        {
            _chatService = chatService;
            _clientService = clientService;
        }

        [HttpPost("create-chat")]
        [Authorize]
        public async Task<IActionResult> CreateChat(string chatName)
        {
            var client = _clientService.GetClientByUserName(HttpContext.User.GetUserName());
            if (client is null) return BadRequest("Current user doesn'nt exist!");

            return await _chatService.CreateChat(chatName, client) ?
                Ok() : BadRequest("Something gone wrong! Please try later.");
        }

        [HttpGet("my-chats")]
        [Authorize]
        public async Task<IActionResult> GetChats()
        {
            var client = _clientService.GetClientByUserName(HttpContext.User.GetUserName());
            if (client is null) return BadRequest("Current user doesn'nt exist!");

            return Ok(_chatService.GetChatsByUsername(client!.UserName));
        }

    }
}
