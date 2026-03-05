using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class MovieCast
{
    public int Id { get; set; }

    public int? IdMovie { get; set; }

    public int? IdCast { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Status { get; set; }

    public virtual Cast? IdCastNavigation { get; set; }

    public virtual Movie? IdMovieNavigation { get; set; }
}
