using backend.Models.ENUM;

namespace backend.Entities;

public partial class User
{
    public int Id { get; set; }

    public int? IdRol { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedAt { get; set; }

    public ENTITY_STATUS Status { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<MovieLike> MovieLikes { get; set; } = new List<MovieLike>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
