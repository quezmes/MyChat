using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyChatAPI.Models.DTO;
using MyChatAPI.Services;
using System.Security.Cryptography;
using System.Text;

namespace MyChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientService _clientDbService;

        public ClientController(IClientService clientDbService)
        {
            _clientDbService = clientDbService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            try
            {
                var client = _clientDbService.LoginClient(model.EmailOrUserName, model.Password);             

                return Ok(client);
            }
            catch (Exception e)
            {

                return Unauthorized(e.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Resgister(RegisterDTO model)
        {
            try
            {
                await _clientDbService.RegisterClientAsync(model.Email, model.UserName, model.Password, model.ConfirmPassword);
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
