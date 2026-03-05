using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class MovieLike
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? IdMovie { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Status { get; set; }

    public virtual Movie? IdMovieNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
