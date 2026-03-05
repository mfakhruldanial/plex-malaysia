using backend.DBContext;
using backend.Models.ENUM;
using backend.Models.Movie;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace backend.Services.Movie
{
    public class PostNewMovie
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;
        private readonly HttpContextUtils _httpUtils;

        public PostNewMovie(
            MovieCatalogContext context,
            LogManager logger,
            HttpContextUtils httpUtils)
        {
            _context = context;
            _logger = logger;
            _httpUtils = httpUtils;
        }

        public async Task<IActionResult> NewMovie(HttpContext httpContext)
        {
            GeneralResponse<Entities.Movie> response = new();
            try
            {
                _logger.LogStart(httpContext);

                MovieRequestModel newMovie = await _httpUtils.GetBodyRequest<MovieRequestModel>(httpContext);
                _logger.LogObjectInformation(httpContext, newMovie);
                if (newMovie is null)
                {
                    _logger.LogInformation(httpContext, "Invalid movie data");
                    response = new()
                    {
                        StatusCode = 400,
                        Message = "Invalid movie data",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                Entities.Movie movieToSave = new()
                {
                    PrimaryImage = newMovie.PrimaryImage,
                    Name = newMovie.Name,
                    Description = newMovie.Description,
                    Quality = newMovie.Quality,
                    Trailer = newMovie.Trailer,
                    Director = newMovie.Director,
                    Rating = newMovie.Rating,
                    Premiere = newMovie.Premiere,
                    Duration = newMovie.Duration,
                    Status = ENTITY_STATUS.ACTIVE,
                    CreatedAt = DateTime.UtcNow.AddHours(-6)
                };

                await _context.Movies.AddAsync(movieToSave);
                await _context.SaveChangesAsync();

                response = new()
                {
                    StatusCode = 201,
                    Message = "Movie created",
                    Object = movieToSave
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier}\nAn error occurred while creating movie",
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
