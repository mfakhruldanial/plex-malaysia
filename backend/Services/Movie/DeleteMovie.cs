using backend.DBContext;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Movie
{
    public class DeleteMovie
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public DeleteMovie(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
        }

        public async Task<IActionResult> Delete(HttpContext httpContext)
        {
            GeneralResponse<Entities.Movie> response = new();
            try
            {
                _logger.LogStart(httpContext);

                int id = _httpUtils.GetIntParam(httpContext, "id");
                _logger.LogInformation(httpContext, $"Searching movie with id: {id}");
                if (id.Equals(0))
                {
                    response = new()
                    {
                        StatusCode = 400,
                        Message = "Invalid movie id",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                Entities.Movie? movie = await _context.Movies.FindAsync(id);
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

                movie.Status = Models.ENUM.ENTITY_STATUS.DELETED;

                await _context.SaveChangesAsync();

                response = new()
                {
                    StatusCode = 201,
                    Message = "Movie delete successfully",
                    Object = new()
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier}\nAn error occurred while deleting user",
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