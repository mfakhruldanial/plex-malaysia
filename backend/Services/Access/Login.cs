using backend.DBContext;
using backend.Entities;
using backend.Models.Access;
using backend.Models.Response;
using backend.Models.Token;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Access
{
    public class Login
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;
        private readonly Security _security;
        private readonly TokenManager _jsonToken;

        public Login(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils,
            Security security,
            TokenManager jsonToken)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
            _security = security;
            _jsonToken = jsonToken;
        }

        public async Task<IActionResult> Auth(HttpContext httpContext)
        {
            GeneralResponse<TokenModel> response = new();
            try
            {
                _logger.LogStart(httpContext);               

                LoginModel loginData = await _httpUtils.GetBodyRequest<LoginModel>(httpContext);
                _logger.LogObjectInformation(httpContext, loginData);

                if (loginData.Equals(null))
                {
                    response = new()
                    {
                        StatusCode = 400,
                        Message = $"Invalid login data",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                User? userFind = await _context.Users.Include(x => x.IdRolNavigation).FirstOrDefaultAsync(u => u.Email == loginData.Email);
                if (userFind is null)
                {
                    response = new()
                    {
                        StatusCode = 404,
                        Message = $"Invalid email",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                if (!_security.VerifyPassword(loginData.Password, userFind.Password!))
                {
                    response = new()
                    {
                        StatusCode = 404,
                        Message = $"Invalid password",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                var token = _jsonToken.GenerateToken(userFind);
                _jsonToken.CreateCookie(httpContext, token);

                response = new()
                {
                    StatusCode = 200,
                    Message = "Login successful",
                    Object = null
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while trying login",
                    Object = null
                };
                return new OkObjectResult(response);
            }
            finally
            {
                _logger.LogEnd(httpContext);
            }
        }
    }
}