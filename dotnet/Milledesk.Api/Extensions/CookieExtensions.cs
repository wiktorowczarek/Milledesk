namespace Milledesk.Api.Extensions
{
    public static class CookieExtensions
    {
        private const string ClientIdCookieName = "clientId";

        public static string GetOrCreateClientId(this HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue(ClientIdCookieName, out var existing))
                return existing;

            var newClientId = Guid.NewGuid().ToString();

            context.Response.Cookies.Append(
                ClientIdCookieName,
                newClientId,
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    SameSite = SameSiteMode.Strict
                });

            return newClientId;
        }
    }
}
