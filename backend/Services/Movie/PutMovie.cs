using backend.DBContext;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Movie
{
    public class PutMovie
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public PutMovie(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
        }

        public async Task<IActionResult> EditMovie(HttpContext httpContext)
        {
            GeneralResponse<Entities.Movie> response = new();
            try
            {
                _logger.LogStart(httpContext);

                Entities.Movie movieEdit = await _httpUtils.GetBodyRequest<Entities.Movie>(httpContext);
                _logger.LogObjectInformation(httpContext, movieEdit);

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

                movie.Name = movieEdit.Name;
                movie.PrimaryImage = movieEdit.PrimaryImage;
                movie.Description = movieEdit.Description;
                movie.Trailer = movieEdit.Trailer;
                movie.Quality = movieEdit.Quality;
                movie.Director = movieEdit.Director;
                movie.Rating = movieEdit.Rating;
                movie.Premiere = movieEdit.Premiere;
                movie.Duration = movieEdit.Duration;

                await _context.SaveChangesAsync();

                response = new()
                {
                    StatusCode = 204,
                    Message = "Movie edited successfully",
                    Object = movie
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier}\nAn error occurred while editing user",
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