namespace backend.Models.Rol
{
    public class RolModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Status { get; set; }
    }
}