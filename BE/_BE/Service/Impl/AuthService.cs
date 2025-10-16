using _BE.Models;
using _BE.Repositories.Interface;
using _BE.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _BE.Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<SystemAccount> _accountRepo;
        private readonly IConfiguration _config;

        public AuthService(ISystemAccountRepository accountRepo, IConfiguration config)
        {
            _accountRepo = accountRepo;
            _config = config;
        }

        public string? Login(string email, string password)
        {
            var user = _accountRepo.GetAll()
                .FirstOrDefault(u => u.AccountEmail == email && u.AccountPassword == password);


            // Kiểm tra admin từ appsettings
            var adminEmail = _config["AdminAccount:Email"];
            if (email == adminEmail && password == _config["AdminAccount:Password"])
            {
                user = new SystemAccount { AccountEmail = adminEmail };
                user.AccountRole = 999; // Admin
            }
            if (user == null) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString()),
            new Claim(ClaimTypes.Email, user.AccountEmail),
            new Claim(ClaimTypes.Role, user.AccountRole.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
