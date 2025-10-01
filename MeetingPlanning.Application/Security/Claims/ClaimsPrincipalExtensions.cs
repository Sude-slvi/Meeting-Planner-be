using System.Security.Claims;

namespace MeetingPlanning.Application.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal userClaims)
        {
            return userClaims.FindFirst("preferred_username")?.Value
                   ?? throw new Exception("Token'da username yok.");
        }
        public static List<string> GetRoles(this ClaimsPrincipal userClaims)
        {
            return userClaims.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }
    }
}
