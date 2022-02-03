using GroupChatApi.Services;
using MyChatAPI.Context;
using MyChatAPI.Models.Database;
using MyChatAPI.Models.DTO;
using System.Security.Cryptography;
using System.Text;

namespace MyChatAPI.Services
{

    public interface IClientService
    {
        Task<LoggedClientDTO> RegisterClientAsync(string email, string username, string password, string confirmPssword);
        LoggedClientDTO LoginClient(string emailOrUsername, string password);
        Client? GetClientByEmail(string email);
        Client? GetClientByUserName(string userName);

    }
    public class ClientService : IClientService
    {

        private readonly ClientDbContext _context;
        private readonly ITokenService _tokenService;

        public ClientService(ClientDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public Client? GetClientByEmail(string email) => _context.Clients.FirstOrDefault(x => x.Email.Equals(email));
        public Client? GetClientByUserName(string userName) => _context.Clients.FirstOrDefault(x => x.UserName.Equals(userName));



        public async Task<LoggedClientDTO> RegisterClientAsync(string email, string username, string password, string confirmPssword)
        {
            if (password != confirmPssword) throw new ArgumentException("Passwords are not the same!");

            var clientByEmail = GetClientByEmail(email);
            if (clientByEmail != null) throw new ArgumentException("User with these email already exist!");

            var clientByUsername = GetClientByEmail(email);
            if (clientByUsername != null) throw new ArgumentException("User with these email already exist!");

            using var hmac = new HMACSHA512();

            var client = new Client
            {
                Email = email,
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return new LoggedClientDTO
            {
                UserName = client.UserName,
                Token = _tokenService.CreateToken(client)
            };
        }

        public LoggedClientDTO LoginClient(string emailOrUsername, string password)
        {
     

            var client = _context.Clients.FirstOrDefault(x => x.UserName == emailOrUsername || x.Email == emailOrUsername)
                         ?? throw new ArgumentNullException("No with specified email or username!");

            using var hmac = new HMACSHA512(client.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != client.PasswordHash[i]) throw new ArgumentException("Invalis password");
            }

            return new LoggedClientDTO
            {
                UserName= client.UserName,
                Token = _tokenService.CreateToken(client)
            };
        }


    }
}
