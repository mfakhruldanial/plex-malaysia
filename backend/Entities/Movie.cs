using backend.Models.ENUM;
using System.Text.Json.Serialization;

namespace backend.Entities;

public partial class Movie
{
    public int Id { get; set; }

    public string? PrimaryImage { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Trailer { get; set; }

    public string? Quality { get; set; }

    public string? Director { get; set; }

    public int? Rating { get; set; }

    public DateTime? Premiere { get; set; }

    public string? Duration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public ENTITY_STATUS? Status { get; set; }

    public virtual ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();

    public virtual ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

    public virtual ICollection<MovieLike> MovieLikes { get; set; } = new List<MovieLike>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
