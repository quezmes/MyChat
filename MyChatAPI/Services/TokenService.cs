using MyChatAPI.Models.Database;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroupChatApi.Services
{
    public interface ITokenService
    {
        string CreateToken(Client client);
    }
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(Client client)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, client.UserName)
            };

            var creditionals = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creditionals,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
