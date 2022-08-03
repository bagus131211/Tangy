using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tangy.Common;
using Tangy.Data.Models;
using Tangy.Models;
using TangyWeb.API.Helpers;

namespace TangyWeb.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly LoginSetting _loginSetting;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<LoginSetting> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _loginSetting = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequest)
        {
            if (signUpRequest == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new AppUser
            {
                UserName = signUpRequest.Email,
                Email = signUpRequest.Email,
                Name = signUpRequest.Name,
                PhoneNumber = signUpRequest.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, signUpRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO
                {
                    IsSignUpSuccessful = false,
                    Errors = result.Errors.Select(s => s.Description)
                });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Constants.Role.Customer);

            if (!roleResult.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO
                {
                    IsSignUpSuccessful = false,
                    Errors = result.Errors.Select(s => s.Description)
                });
            }

            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequest)
        {
            if (signInRequest == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequest.UserName, signInRequest.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new SignInResponseDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid authentication"
                });
            }

            var user = await _userManager.FindByNameAsync(signInRequest.UserName);

            if (user is null)
                return Unauthorized(new SignInResponseDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid authentication"
                });

            var credential = GetCredentials();
            var claims = await GetClaims(user);

            var tokenOptions = new JwtSecurityToken(
                   _loginSetting.ValidIssuer,
                   _loginSetting.ValidAudience,
                   claims,
                   expires: DateTime.Now.AddDays(3),
                   signingCredentials: credential
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new SignInResponseDTO
            {
                IsSuccess = true,
                Token = token,
                User = new()
                {
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id
                }
            });
        }

        SigningCredentials GetCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_loginSetting.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha384Signature);
        }

        async Task<List<Claim>> GetClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id)
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
