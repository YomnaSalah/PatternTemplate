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

        public static Guid LanguageId(this HttpContext httpContext)
        {
            if (httpContext == null) return Guid.Empty;
            // default ar 
            return Guid.TryParse(httpContext.Request.Cookies[AppConstants.LanguageIdCookie], out Guid id) ? id : Guid.Empty;
        }
        public static void ChangeLanguage(this HttpResponse Response, Guid LangId, string code, bool isRtl)
        {
            Response.Cookies.Delete(AppConstants.LanguageCodeCookie);
            Response.Cookies.Delete(AppConstants.LanguageIdCookie);

            Response.Cookies.AppendCookie(AppConstants.LanguageCodeCookie, code);
            Response.Cookies.AppendCookie(AppConstants.LanguageIdCookie, LangId + "");
            Response.Cookies.AppendCookie(AppConstants.LanguageRtlCookie, isRtl.ToString());
        }

        /// <summary>
        /// Get Language Code From Cookie If Not Exists Return Default
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string LanguageCode(this HttpContext httpContext)
        {
            return httpContext.Request.Cookies[AppConstants.LanguageCodeCookie]?.ToLower() ?? "ar";
        }
        public static bool LanguageIsArabic(this HttpContext httpContext)
        {
            return httpContext.LanguageCode() == "ar";
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
