using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Personal_Finance_Tracker___Enterprise_Edition.DTO;
using Personal_Finance_Tracker___Enterprise_Edition.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Personal_Finance_Tracker___Enterprise_Edition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = config;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
            var user = await _userManager.FindByEmailAsync(loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Email not found.");
            }

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid login Credentials.");
            }
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, user.Role));

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };

            var secretKey = _configuration["JwtSettings:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey??""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var ExpireMinutes = Convert.ToInt16(_configuration["JwtSettings:AccessTokenExpirationMinutes"]);
            var ExpireTime = DateTime.UtcNow.AddMinutes(ExpireMinutes);
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    expires: ExpireTime,
                    signingCredentials: credentials);
            var Token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                AccessToken = Token,
                username=user.UserName
            });
        }

        [HttpPost("Loginas")]
        public async Task<IActionResult> Loginas([FromBody] LoginDTO loginDto)
        {

            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Sid, user.Id), new Claim(ClaimTypes.Role,user.Role) };
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var ExpireMinutes = Convert.ToInt16(_configuration["JwtSettings:AccessTokenExpirationMinutes"]);
            var ExpireTime = DateTime.UtcNow.AddMinutes(ExpireMinutes);
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    expires: ExpireTime,
                    signingCredentials: credentials);
            var Token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                AccessToken = Token,
                username = user.UserName
            });
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] SignUpDTO signUpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                UserName = signUpDto.Username,
                Email = signUpDto.Email,
                Role = signUpDto.Role,
                IsActive = true,
                PasswordHash = "",
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok("User successfully signed up");
        }

        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // TODO: logout

            return Ok("Logged out successfully.");
        }

    }
}
