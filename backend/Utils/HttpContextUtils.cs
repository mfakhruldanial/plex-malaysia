using System.Text.Json;

namespace backend.Utils
{
    public class HttpContextUtils
    {
        private readonly JsonManager _json;

        public HttpContextUtils(
            JsonManager json)
        {
            _json = json;
        }

        public string GetStringParam(HttpContext httpContext, string key)
        {
            string value = httpContext.Request.RouteValues[key]?.ToString() ?? string.Empty;
            return value;
        }

        public int GetIntParam(HttpContext httpContext, string key)
        {
            string value = httpContext.Request.RouteValues[key]?.ToString() ?? string.Empty;
            return int.Parse(value);
        }

        public async Task<T> GetBodyRequest<T>(HttpContext httpContext)
        {
            string body = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
            T bodyObject = _json.Deserialize<T>(body);
            return bodyObject;
        }
    }
}
