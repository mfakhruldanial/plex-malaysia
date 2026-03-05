using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class Rol
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
