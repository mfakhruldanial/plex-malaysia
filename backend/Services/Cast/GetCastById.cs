using backend.DBContext;
using backend.Models.Cast;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Cast
{
    public class GetCastById
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public GetCastById(
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
            GeneralResponse<CastModel> response = new();
            try
            {
                _logger.LogStart(httpContext);

                int id = _httpUtils.GetIntParam(httpContext, "id");
                _logger.LogInformation(httpContext, $"Id: {id}");

                if (id.Equals(0))
                {
                    _logger.LogInformation(httpContext, $"Id is 0");
                    response = new()
                    {
                        StatusCode = 400,
                        Message = "Id is required",
                        Object = null
                    };
                    return new ObjectResult(response);

                }

                _logger.LogInformation(httpContext, $"Searching user with id: {id}");
                CastModel? castEntity =
                    await _context.Casts.Where(x => 
                                                    x.Id.Equals(id) &&
                                                    x.Status.Equals(ENTITY_STATUS.ACTIVE))
                                        .Select(x =>
                                                    new CastModel
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        Status = x.Status,
                                                        CreatedAt = x.CreatedAt,
                                                    })
                                        .FirstOrDefaultAsync();

                if (castEntity is null)
                {
                    _logger.LogInformation(httpContext, $"Cast not found");
                    response = new()
                    {
                        Message = "Cast not found",
                        StatusCode = 404,
                        Object = null
                    };
                    return new ObjectResult(response);
                }

                _logger.LogInformation(httpContext, $"Cast entity");
                _logger.LogObjectInformation(httpContext, castEntity);
                response = new()
                {
                    Message = "success",
                    Object = castEntity,
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
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while obtaining cast by id",
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
