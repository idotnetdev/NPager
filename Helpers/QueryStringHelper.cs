using System.Web;

namespace NPager.Helpers
{
    public class QueryStringHelper
    {
        public static string AddUpdateQueryStringGetUrl(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key) && value == null)
            {
                return HttpContext.Current.Request.Url.ToString();
            }
            var nameValues = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
            nameValues.Set(key, value.ToString());
            var absoluteUrl = HttpContext.Current.Request.Url.AbsolutePath;
            var updatedQueryString = "?" + nameValues;
            return absoluteUrl + updatedQueryString;
        }
    }
}
