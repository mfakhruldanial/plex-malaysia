using backend.DBContext;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Models.UserApp;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.UserApp
{
    public class GetAllUsers
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;

        public GetAllUsers(
            MovieCatalogContext context,
            LogManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> GetUsers(HttpContext httpContext)
        {
            GeneralResponse<IEnumerable<UserModel>> response = new();
            try
            {
                _logger.LogStart(httpContext);

                IEnumerable<UserModel> users =
                await _context.Users.Where(x =>
                                                x.Status.Equals(ENTITY_STATUS.ACTIVE))
                                    .Include(x => x.IdRolNavigation)
                                    .Select(x => new UserModel
                                    {
                                        Id = x.Id,
                                        FirstName = x.FirstName,
                                        LastName = x.LastName,
                                        Email = x.Email,
                                        Rol = x.IdRolNavigation!.Name
                                    })
                                    .ToArrayAsync();

                if (!users.Any())
                {
                    response = new()
                    {
                        StatusCode = 404,
                        Message = "Users not found",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                response = new()
                {
                    StatusCode = 200,
                    Message = "Users found",
                    Object = users
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while obtaining users",
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