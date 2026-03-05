using backend.DBContext;
using backend.Models.Cast;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Cast
{
    public class PostNewCast
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public PostNewCast(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
        }

        public async Task<IActionResult> NewCast(HttpContext httpContext)
        {
            GeneralResponse<Entities.Cast> response = new();
            try
            {
                _logger.LogStart(httpContext);

                Entities.Cast cast = await _httpUtils.GetBodyRequest<Entities.Cast>(httpContext);
                _logger.LogObjectInformation(httpContext, cast);

                if (cast is null)
                {
                    _logger.LogInformation(httpContext, "Invalid cast data");
                    response = new()
                    {
                        StatusCode = 400,
                        Message = "Invalid cast data",
                        Object = null
                    };
                    return new ObjectResult(response);
                }

                cast.Status = ENTITY_STATUS.ACTIVE;
                cast.CreatedAt = DateTime.UtcNow.AddHours(-6);

                _logger.LogInformation(httpContext, "Saving data...");
                await _context.Casts.AddAsync(cast);
                await _context.SaveChangesAsync();
                _logger.LogInformation(httpContext, "Data saved");

                response = new()
                {
                    StatusCode = 201,
                    Message = "Cast created successfully",
                    Object = cast
                };

                return new ObjectResult(response);
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
