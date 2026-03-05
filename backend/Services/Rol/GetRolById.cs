using backend.DBContext;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Rol
{
    public class GetRolById
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public GetRolById(
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
            GeneralResponse<Entities.Rol> response = new();
            try
            {
                _logger.LogStart(httpContext);

                int id = _httpUtils.GetIntParam(httpContext, "id");
                _logger.LogInformation(httpContext, $"Searching rol with id: {id}");

                Entities.Rol? rol =
                    await _context.Rols.Where(x => 
                                                   x.Status.Equals(ENTITY_STATUS.ACTIVE) && 
                                                   x.Id.Equals(id))
                                       .FirstOrDefaultAsync();

                _logger.LogObjectInformation(httpContext, rol);

                if (rol is null)
                {
                    _logger.LogInformation(httpContext, "Rol not found");
                    response = new()
                    {
                        StatusCode = 404,
                        Message = "Rol not found",
                        Object = null
                    };

                    return new OkObjectResult(response);
                }

                response = new()
                {
                    StatusCode = 200,
                    Message = "Success",
                    Object = rol
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while obtaining rol by id",
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