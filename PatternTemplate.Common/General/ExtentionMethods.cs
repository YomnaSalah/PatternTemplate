using Microsoft.AspNetCore.Http;

namespace PatternTemplate.Common
{
    public static class ExtentionMethods
    {
        #region User
        public static Guid UserId(this IHttpContextAccessor httpContext)
        {
            return (httpContext?.HttpContext?.User?.Identity?.IsAuthenticated ?? false) ? Guid.Parse(httpContext?.HttpContext.User.Claims.FirstOrDefault().Value) : Guid.Empty;
        }
        public static Guid UserId(this HttpContext httpContext)
        {
            return (httpContext?.User?.Identity?.IsAuthenticated ?? false) ? Guid.Parse(httpContext?.User.Identity.Name) : Guid.Empty;
        }
        public static void AppendCookie(this IResponseCookies responseCookies, string key, string value, bool IsExpire = false)
        {
            var cOption = new CookieOptions()
            {
                HttpOnly = false,
                Path = "/",
                //TODO please un comment the next line if y will use HTTPS
                // Secure = true
            };

            if (!IsExpire)
            {
                cOption.Expires = DateTime.Now.AddDays(1);
            }


            responseCookies.Append(key, value, cOption);
        }



        #endregion

    }
}
