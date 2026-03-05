using backend.DBContext;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Rol
{
    public class GetAllRoles
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;

        public GetAllRoles(
            MovieCatalogContext context,
            LogManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> GetAll(HttpContext httpContext)
        {
            GeneralResponse<IEnumerable<Entities.Rol>> response = new();
            try
            {
                _logger.LogStart(httpContext);

                IEnumerable<Entities.Rol> roles = 
                    await _context.Rols.Where(x => 
                                                   x.Status.Equals(ENTITY_STATUS.ACTIVE))
                                       .ToListAsync();
                _logger.LogObjectInformation(httpContext, roles);

                if (!roles.Any())
                {
                    response = new()
                    {
                        StatusCode = 404,
                        Message = "Roles not found",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }                

                response = new()
                {
                    StatusCode = 200,
                    Message = "Success",
                    Object = roles
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while obtaining roles",
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