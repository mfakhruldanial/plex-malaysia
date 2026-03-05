using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class Review
{
    public int Id { get; set; }

    public int? IdMovie { get; set; }

    public int? IdUser { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Status { get; set; }

    public virtual Movie? IdMovieNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
