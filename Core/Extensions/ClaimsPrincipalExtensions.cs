using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetCurrentUserId(this ClaimsPrincipal principal)
        {
            if (principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier)
                && int.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return userId;
            }

            return 0;
        }
    }
}
