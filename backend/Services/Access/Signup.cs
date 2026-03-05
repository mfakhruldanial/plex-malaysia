using backend.DBContext;
using backend.Entities;
using backend.Models.Access;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Models.Token;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Access
{
    public class Signup
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;
        private readonly Security _security;
        private readonly TokenManager _jsonToken;

        public Signup(
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

        public async Task<IActionResult> Register(HttpContext httpContext)
        {
            GeneralResponse<TokenModel> response = new();
            try
            {
                _logger.LogStart(httpContext);

                SignupModel signupData = await _httpUtils.GetBodyRequest<SignupModel>(httpContext);
                _logger.LogObjectInformation(httpContext, signupData);

                if (signupData.Equals(null))
                {
                    response = new()
                    {
                        StatusCode = 400,
                        Message = $"Invalid signup data",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                User? userFind = await _context.Users.FirstOrDefaultAsync(u => u.Email == signupData.Email);
                if (userFind is not null)
                {
                    response = new()
                    {
                        StatusCode = 400,
                        Message = $"User already exists",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                if (!signupData.Password.Equals(signupData.ConfirmPassword))
                {
                    response = new()
                    {
                        StatusCode = 400,
                        Message = $"Passwords do not match",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                User newUser = new()
                {
                    FirstName = signupData.FirstName,
                    LastName = signupData.LastName,
                    Email = signupData.Email,
                    Password = _security.HashPassword(signupData.Password),
                    IdRol = 3,
                    CreatedAt = DateTime.UtcNow.AddHours(-6),
                    Status = ENTITY_STATUS.ACTIVE
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                response = new()
                {
                    StatusCode = 200,
                    Message = $"Successfully registered user",
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
                    Message = $"TraceId: ${httpContext.TraceIdentifier} \nAn error has occurred",
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