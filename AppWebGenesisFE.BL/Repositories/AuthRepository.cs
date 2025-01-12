using AppWebGenesisFE.Database.Data;
using AppWebGenesisFE.Models.Entities;
using AppWebGenesisFE.Models.Entities.Tenant;
using AppWebGenesisFE.Models.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.BL.Repositories
{
    public interface IAuthRepository
    {
        Task<UserModel> RegisterTenantAsync(RegisterTenantModel model);
        Task<UserModel> LoginAsync(LoginModel model);


        Task RemoveRefreshTokenByUserID(long userID);
        Task AddRefreshTokenModel(RefreshTokenModel refreshTokenModel);
        Task<RefreshTokenModel> GetRefreshTokenModel(string refreshToken);

    }
    public class AuthRepository(AppDbContext AppDbContext, IPasswordHasher<UserModel> passwordHasher) : IAuthRepository
    {
        public async Task AddRefreshTokenModel(RefreshTokenModel refreshTokenModel)
        {
            await AppDbContext.RefreshTokens.AddAsync(refreshTokenModel);
            await AppDbContext.SaveChangesAsync();
        }

        public Task<RefreshTokenModel> GetRefreshTokenModel(string refreshToken)
        {
            return AppDbContext.RefreshTokens.Include(n => n.User).ThenInclude(n => n.UserRoles).ThenInclude(n => n.Role).FirstOrDefaultAsync(n => n.RefreshToken == refreshToken);
        }

        public async Task<UserModel> LoginAsync(LoginModel model)
        {
            return (await AppDbContext.Users
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync(u => u.Email == model.Email))!;

        }

        public async Task<UserModel> RegisterTenantAsync(RegisterTenantModel model)
        {
            using var transaction = await AppDbContext.Database.BeginTransactionAsync();
            try
            {
                // Crear tenant
                var tenant = new TenantModel
                {
                    Name = model.CompanyName,
                    Identification = model.Identification,
                    IsActive = true
                };

                AppDbContext.Tenants.Add(tenant);
                await AppDbContext.SaveChangesAsync();

                // Crear usuario administrador
                var user = new UserModel
                {
                    TenantId = tenant.ID,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Rol = "Admin",
                    IsActive = true
                };
                user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

                AppDbContext.Users.Add(user);
                await AppDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return user;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task RemoveRefreshTokenByUserID(long userID)
        {
            var refreshToken = AppDbContext.RefreshTokens.FirstOrDefault(n => n.UserID == userID);
            if (refreshToken != null)
            {
                AppDbContext.RemoveRange(refreshToken);
                await AppDbContext.SaveChangesAsync();
            }
        }
    }
}
