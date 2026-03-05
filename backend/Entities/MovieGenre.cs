using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class MovieGenre
{
    public int Id { get; set; }

    public int? IdMovie { get; set; }

    public int? IdGenre { get; set; }

    public int? Status { get; set; }

    public virtual Genre? IdGenreNavigation { get; set; }

    public virtual Movie? IdMovieNavigation { get; set; }
}
