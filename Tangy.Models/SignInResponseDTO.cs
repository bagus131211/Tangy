namespace Tangy.Models
{
    public class SignInResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public UserDTO User { get; set; }
    }
}
