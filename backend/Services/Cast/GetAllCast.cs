using backend.DBContext;
using backend.Models.Cast;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Cast
{
    public class GetAllCast
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;

        public GetAllCast(
            MovieCatalogContext context,
            LogManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> GetAll(HttpContext httpContext)
        {
            GeneralResponse<IEnumerable<CastModel>> response = new();
            try
            {
                _logger.LogStart(httpContext);

                List<CastModel> castEntities = 
                    await _context.Casts.Where(x => 
                                                    x.Status.Equals(ENTITY_STATUS.ACTIVE))
                                        .Select(x => 
                                                     new CastModel
                                                     {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        Status = x.Status,
                                                        CreatedAt = x.CreatedAt,
                                                     })
                                        .ToListAsync();

                if (!castEntities.Any())
                {
                    response = new()
                    {
                        Message = "Casts not found",
                        StatusCode = 404,
                        Object = null
                    };
                    return new ObjectResult(response);
                }

                response = new()
                {
                    Message = "success",
                    Object = castEntities,
                    StatusCode = 200
                };
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while obtaining cast",
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
