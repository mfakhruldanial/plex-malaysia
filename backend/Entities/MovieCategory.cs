using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class MovieCategory
{
    public int Id { get; set; }

    public int? IdMovie { get; set; }

    public int? IdCategory { get; set; }

    public int? Status { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual Movie? IdMovieNavigation { get; set; }
}
