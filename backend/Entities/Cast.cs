using backend.Models.ENUM;
using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class Cast
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public ENTITY_STATUS Status { get; set; }

    public virtual ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();
}
