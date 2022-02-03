using System.Security.Claims;

namespace MyChatAPI.Extensions
{
    public static class ClaimsPrincipaleExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }
}
