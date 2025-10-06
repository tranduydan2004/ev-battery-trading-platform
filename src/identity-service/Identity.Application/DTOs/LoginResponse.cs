namespace Identity.Application.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
