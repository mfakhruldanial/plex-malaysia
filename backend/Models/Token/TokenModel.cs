namespace backend.Models.Token
{
    public class TokenModel
    {
        public int Id { get; set; }
        public string AccessToken { get; set; } = null!;
    }
}
