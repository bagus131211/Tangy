namespace Tangy.Models
{
    public class SignUpResponseDTO
    {
        public bool IsSignUpSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
