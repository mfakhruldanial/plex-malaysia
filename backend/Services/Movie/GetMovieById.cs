using backend.DBContext;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Movie
{
    public class GetMovieById
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public GetMovieById(
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
            GeneralResponse<Entities.Movie> response = new();
            try
            {
                _logger.LogStart(httpContext);

                int id = _httpUtils.GetIntParam(httpContext, "id");
                _logger.LogInformation(httpContext, $"Searching movie with id: {id}");

                Entities.Movie? movie =
                    await _context.Movies.Where(x => 
                                                    x.Status.Equals(ENTITY_STATUS.ACTIVE) && 
                                                    x.Id.Equals(id))
                                         .FirstOrDefaultAsync();
                _logger.LogObjectInformation(httpContext, movie);

                if (movie is null)
                {
                    _logger.LogInformation(httpContext, "Movie not found");
                    response = new()
                    {
                        StatusCode = 404,
                        Message = "Movie not found",
                        Object = null
                    };

                    return new OkObjectResult(response);
                }

                response = new()
                {
                    StatusCode = 200,
                    Message = "Success",
                    Object = movie!
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while obtaining movie by id",
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