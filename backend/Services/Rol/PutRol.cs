using backend.DBContext;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Rol
{
    public class PutRol
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public PutRol(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
        }

        public async Task<IActionResult> Edit(HttpContext httpContext)
        {
            GeneralResponse<Entities.Rol> response = new();
            try
            {
                _logger.LogStart(httpContext);

                Entities.Rol rolEdit = await _httpUtils.GetBodyRequest<Entities.Rol>(httpContext);
                _logger.LogObjectInformation(httpContext, rolEdit);

                int id = _httpUtils.GetIntParam(httpContext, "id");
                _logger.LogInformation(httpContext, $"Searching rol with id: {id}");
                if (id.Equals(0))
                {
                    response = new()
                    {
                        StatusCode = 400,
                        Message = "Invalid rol id",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                Entities.Rol? rol = await _context.Rols.FindAsync(id);
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

                rol.Name = rolEdit.Name;
                rol.Status = rolEdit.Status;
                rol.CreatedAt = rolEdit.CreatedAt;

                await _context.SaveChangesAsync();

                response = new()
                {
                    StatusCode = 200,
                    Message = "Rol edited successfully",
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
                    Message = $"TraceId: {httpContext.TraceIdentifier}\nAn error occurred while editing rol",
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