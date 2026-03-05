using backend.DBContext;
using backend.Models.ENUM;
using backend.Models.Response;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Movie
{
    public class GetAllMovies
    {
        private readonly MovieCatalogContext _context;
        private readonly LogManager _logger;

        public GetAllMovies(
            MovieCatalogContext context,
            LogManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> GetAll(HttpContext httpContext)
        {
            GeneralResponse<IEnumerable<Entities.Movie>> response = new();
            try
            {
                _logger.LogStart(httpContext);

                IEnumerable<Entities.Movie> movieEntities = 
                    await _context.Movies.Where(x => 
                                                    x.Status.Equals(ENTITY_STATUS.ACTIVE))
                                         .Include(x => x.MovieGenres)
                                            .ThenInclude(x => x.IdGenreNavigation)
                                         .Include(x => x.MovieCasts)
                                            .ThenInclude(x => x.IdCastNavigation)
                                         .Include(x => x.MovieCategories)
                                            .ThenInclude(x => x.IdCategoryNavigation)
                                         .Include(x => x.Reviews)
                                         .Include(x => x.Watchlists)
                                         .Include(x => x.MovieLikes)
                                         .ToListAsync();
                _logger.LogObjectInformation(httpContext, movieEntities);

                if (!movieEntities.Any())
                {
                    response = new()
                    {
                        StatusCode = 404,
                        Message = "Movies not found",
                        Object = null
                    };
                    return new OkObjectResult(response);
                }

                response = new()
                {
                    StatusCode = 200,
                    Message = "Success",
                    Object = movieEntities!
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(httpContext, ex.Message);
                response = new()
                {
                    StatusCode = 500,
                    Message = $"TraceId: {httpContext.TraceIdentifier} \nAn error occurred while obtaining movies",
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
