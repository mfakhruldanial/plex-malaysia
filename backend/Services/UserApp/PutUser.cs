using backend.DBContext;
using backend.Entities;
using backend.Models.Response;
using backend.Models.UserApp;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.UserApp
{
    public class PutUser
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;
        private readonly Security _security;

        public PutUser(
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

        public async Task<IActionResult> EditUser(HttpContext httpContext)
        {
            GeneralResponse<UserModel> response = new();
            try
            {
                _logger.LogStart(httpContext);

                User userEdit = await _httpUtils.GetBodyRequest<User>(httpContext);
                _logger.LogObjectInformation(httpContext, userEdit);

                int id = _httpUtils.GetIntParam(httpContext, "id");
                _logger.LogInformation(httpContext, $"Searching user with id: {id}");
                if (id.Equals(0))
                {
                    response = new()
                    {
                        StatusCode = 400,
                        Message = "Invalid user id",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }
                
                User? user = await _context.Users.FindAsync(id);
                _logger.LogObjectInformation(httpContext, user);
                if (user is null)
                {
                    response = new()
                    {
                        StatusCode = 404,
                        Message = "User not found",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                user = userEdit;
                await _context.SaveChangesAsync();

                response = new()
                {
                    StatusCode = 204,
                    Message = "User edited successfully",
                    Object = new UserModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    }
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while editing user",
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