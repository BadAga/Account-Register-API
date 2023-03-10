namespace UniversalUserAPI.Models.DTOs
{
    public class LoginDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;       
    }
}
