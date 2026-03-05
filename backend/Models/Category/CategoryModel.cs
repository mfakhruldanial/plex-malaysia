namespace backend.Models.Category
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Status { get; set; }
    }
}
