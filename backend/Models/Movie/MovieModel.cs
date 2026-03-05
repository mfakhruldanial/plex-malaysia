using backend.Models.ENUM;

namespace backend.Models.Movie
{
    public class MovieModel
    {
        public int Id { get; set; }

        public byte[]? PrimaryImage { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Trailer { get; set; }

        public string? Director { get; set; }

        public int? Rating { get; set; }

        public DateTime? Premiere { get; set; }

        public string? Duration { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
