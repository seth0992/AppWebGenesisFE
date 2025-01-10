using AppWebGenesisFE.BL.Repositories;
using AppWebGenesisFE.Database.Data;
using AppWebGenesisFE.Models.Entities.Tenant;
using AppWebGenesisFE.Models.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.BL.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginModel model);
        Task<AuthResponse> RegisterTenantAsync(RegisterTenantModel model);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string token);
    }

    public class AuthService(IAuthRepository authRepository, IConfiguration configuration, IPasswordHasher<UserModel> passwordHasher) : IAuthService
    {
        public async Task<AuthResponse> LoginAsync(LoginModel model)
        {
            var user = await authRepository.LoginAsync(model);

            if (user == null)
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var result = passwordHasher.VerifyHashedPassword(
                user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Credenciales inválidas");

            return await GenerateAuthResponseAsync(user);
        }

        public Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> RegisterTenantAsync(RegisterTenantModel model)
        {
           
            try
            {
                var user = await authRepository.RegisterTenantAsync(model);
             
                return await GenerateAuthResponseAsync(user);
            }
            catch
            {               
                throw;
            }
        }

        public Task RevokeTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        private async Task<AuthResponse> GenerateAuthResponseAsync(UserModel user)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.ID.ToString()),
            new(ClaimTypes.Email, user.Email),
            new("TenantId", user.TenantId.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration["JWT:Secret"]!)!);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                User = user,
                Tenant = user.Tenant
            };
        }
    }
}
