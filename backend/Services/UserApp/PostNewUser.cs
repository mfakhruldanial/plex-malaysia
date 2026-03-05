using backend.DBContext;
using backend.Entities;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Models.UserApp;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserApp
{
    public class PostNewUser
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;
        private readonly Security _security;

        public PostNewUser(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils,
            Security security)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
            _security = security;
        }

        public async Task<IActionResult> NewUser(HttpContext httpContext)
        {
            GeneralResponse<UserModel> response = new();
            try
            {
                _logger.LogStart(httpContext);

                User user = await _httpUtils.GetBodyRequest<User>(httpContext);
                _logger.LogObjectInformation(httpContext, user);

                if (user is null)
                {
                    _logger.LogInformation(httpContext, "Invalid user data");
                    response = new()
                    {
                        StatusCode = 400,
                        Message = "Invalid user data",
                        Object = null
                    };
                    return new ObjectResult(response);
                }

                User? userFind = await _context.Users.FirstOrDefaultAsync(u => u.Email == user!.Email);
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

                User newUser = new()
                {
                    FirstName = user!.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = _security.HashPassword(user.Password!),
                    IdRol = 3,
                    CreatedAt = DateTime.UtcNow.AddHours(-6),
                    Status = ENTITY_STATUS.ACTIVE
                };

                await _context.Users.AddAsync(newUser!);
                await _context.SaveChangesAsync();
                _logger.LogInformation(httpContext, "User created successfully");

                UserModel userCreated = new()
                {
                    FirstName = user!.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Rol = user.IdRolNavigation!.Name
                };

                response = new()
                {
                    StatusCode = 201,
                    Message = "User created successfully",
                    Object = userCreated
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while creating user",
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