using backend.DBContext;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Models.Rol;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Rol
{
    public class PostNewRol
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public PostNewRol(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
        }

        public async Task<IActionResult> New(HttpContext httpContext)
        {
            GeneralResponse<Entities.Rol> response = new();
            try
            {
                _logger.LogStart(httpContext);

                RolRequestModel newRol = await _httpUtils.GetBodyRequest<RolRequestModel>(httpContext);
                _logger.LogObjectInformation(httpContext, newRol);
                if (newRol is null)
                {
                    _logger.LogInformation(httpContext, "Invalid rol data");
                    response = new()
                    {
                        StatusCode = 400,
                        Message = "Invalid rol data",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                Entities.Rol rolToSave = new()
                {
                    Name = newRol.Name,
                    Status = (int?)ENTITY_STATUS.ACTIVE
                };

                await _context.Rols.AddAsync(rolToSave);
                await _context.SaveChangesAsync();

                response = new()
                {
                    StatusCode = 201,
                    Message = "Rol created successfully",
                    Object = rolToSave
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier}\nAn error occurred while creating rol",
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