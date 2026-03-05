using backend.Models.ENUM;

namespace backend.Models.Cast
{
    public class CastModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? CreatedAt { get; set; }

        public ENTITY_STATUS Status { get; set; }
    }
}
