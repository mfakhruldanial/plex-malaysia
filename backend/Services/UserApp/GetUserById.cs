using backend.DBContext;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Models.UserApp;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserApp
{
    public class GetUserById
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public GetUserById(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
        }

        public async Task<IActionResult> GetById(HttpContext httpContext)
        {
            GeneralResponse<UserModel> response = new();
            try
            {
                _logger.LogStart(httpContext);

                int id = _httpUtils.GetIntParam(httpContext, "id");
                _logger.LogInformation(httpContext, $"Searching user with id: {id}");

                UserModel? user =
                    await _context.Users.Where(x =>
                                                    x.Id.Equals(id) &&
                                                    x.Status.Equals(ENTITY_STATUS.ACTIVE))
                                        .Select(x =>
                                                    new UserModel
                                                    {
                                                        Id = x.Id,
                                                        FirstName = x.FirstName,
                                                        LastName = x.LastName,
                                                        Email = x.Email,
                                                        Rol = x.IdRolNavigation!.Name
                                                    })
                                        .FirstOrDefaultAsync();
                _logger.LogObjectInformation(httpContext, user);

                if (user is null)
                {
                    _logger.LogInformation(httpContext, "User not found");
                    response = new()
                    {
                        StatusCode = 404,
                        Message = "User not found",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                response = new()
                {
                    StatusCode = 200,
                    Message = "User found",
                    Object = user
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while obtaining user by id",
                    Object = null
                };

                return new ObjectResult(response);
            }
            finally
            {
                _logger.LogEnd(httpContext);
            }
        }
    }
}