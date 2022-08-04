using Tangy.Models;

namespace TangyWeb.Client.Service
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequest);
        Task<SignInResponseDTO> Login(SignInRequestDTO signInRequest);
        Task Logout();
    }
}
