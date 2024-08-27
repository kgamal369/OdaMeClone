using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OdaMeClone.Data;
using OdaMeClone.Dtos;
using OdaMeClone.Models;
using OdaMeClone.Services;

namespace OdaMeClone.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
        {
        private readonly OdaDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public AccountController(
            OdaDbContext context,
            IPasswordHasher passwordHasher,
            IConfiguration configuration,
            ILogger<AccountController> logger) : base(logger)
            {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
            {
            if (await _context.Users.AnyAsync(u => u.Username == userDto.Username || u.Email == userDto.Email))
                {
                return HandleError("Username or Email is already in use.");
                }

            var user = new User
                {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = _passwordHasher.HashPassword(userDto.Password),
                RoleId = userDto.RoleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Optionally send an email confirmation link here

            return HandleSuccess("User registered successfully. Please confirm your email.");
            }

        // Login and issue JWT
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
            {
            var user = await _context.Users.Include(u => u.Role)
                                            .SingleOrDefaultAsync(u => u.Username == loginDto.Username || u.Email == loginDto.Email);

            if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
                {
                return Unauthorized("Invalid credentials.");
                }

            if (!user.EmailConfirmed)
                {
                return HandleError("Email not confirmed. Please check your inbox.");
                }

            var token = GenerateJwtToken(user);
            return HandleSuccess(new { Token = token });
            }

        // Generate a JWT token for the authenticated user
        private string GenerateJwtToken(User user)
            {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
                {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            }

        // Password Reset Request
        [HttpPost("password-reset-request")]
        public async Task<IActionResult> PasswordResetRequest(PasswordResetRequestDto resetRequestDto)
            {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == resetRequestDto.Email);
            if (user == null)
                {
                return HandleError("No user found with the provided email.");
                }

            // Generate a password reset token and set its expiry
            user.PasswordResetToken = GenerateResetToken();
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);

            await _context.SaveChangesAsync();

            // Optionally send the reset token via email

            return HandleSuccess("Password reset instructions sent to your email.");
            }

        // Reset Password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
            {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.PasswordResetToken == resetPasswordDto.Token);
            if (user == null || user.PasswordResetTokenExpiry < DateTime.UtcNow)
                {
                return HandleError("Invalid or expired password reset token.");
                }

            user.PasswordHash = _passwordHasher.HashPassword(resetPasswordDto.NewPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            await _context.SaveChangesAsync();

            return HandleSuccess("Password reset successful.");
            }

        // Email Confirmation
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
            {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == confirmEmailDto.Email && u.PasswordResetToken == confirmEmailDto.Token);
            if (user == null || user.PasswordResetTokenExpiry < DateTime.UtcNow)
                {
                return HandleError("Invalid or expired email confirmation token.");
                }

            user.EmailConfirmed = true;
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            await _context.SaveChangesAsync();

            return HandleSuccess("Email confirmed successfully.");
            }

        private string GenerateResetToken()
            {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            }
        }
    }
