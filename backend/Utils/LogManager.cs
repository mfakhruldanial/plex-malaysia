using System.Runtime.CompilerServices;
using System.Text.Json;

namespace backend.Utils
{
    public class LogManager
    {
        private readonly ILogger<LogManager> _logger;

        public LogManager(
            ILogger<LogManager> logger)
        {
            _logger = logger;
        }

        public void LogError(HttpContext httpContext, string message)
        {
            _logger.LogError($"{httpContext.TraceIdentifier} - An error has occurred {message}");
        }

        public void LogInformation(HttpContext httpContext, string message)
        {
            _logger.LogInformation($"{httpContext.TraceIdentifier} - {message}");
        }

        public void LogObjectInformation(HttpContext httpContext, object? obj)
        {
            string objectTxt = JsonSerializer.Serialize(obj);
            _logger.LogInformation($"{httpContext.TraceIdentifier} - The object has the following value: {objectTxt}");
        }

        public void LogStart(HttpContext httpContext, [CallerMemberName] string methodName = "")
        {
            _logger.LogInformation($"{httpContext.TraceIdentifier} - Starting method {methodName}");
        }

        public void LogEnd(HttpContext httpContext, [CallerMemberName] string methodName = "")
        {
            _logger.LogInformation($"{httpContext.TraceIdentifier} - Finishing method {methodName}");
        }
    }
}
