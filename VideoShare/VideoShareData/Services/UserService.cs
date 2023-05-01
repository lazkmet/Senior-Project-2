using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoShareData.DTOs;
using VideoShareData.Helpers;
using VideoShareData.Models;
using VideoShareData.Enums;

namespace VideoShareData.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByLoginAsync(LoginModel loginValues);
        Task<int> CreateUserAsync(NewUserModel newUser);
        Task<User?> GetUserByIdAsync(int userIdParam);
        //TODO: Delete User Async
    }
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<WebAppDbContext> _contextFactory;

        public UserService(IDbContextFactory<WebAppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<User?> GetUserByLoginAsync(LoginModel loginValues)
        {
            using var context = _contextFactory.CreateDbContext();
            User? User = await context.Users.SingleOrDefaultAsync(u => u.EmailAddress == loginValues.EmailAddress);
            if (User != null) {
                //If passwords do not match, return null
                if (!EncryptionHelper.CheckPasswordHash(loginValues.Password, User.EncryptedPassword)) {
                    return null;
                };
            };
            return User;
        }
        public async Task<User?> GetUserByIdAsync(int userIdParam)
        {
            using var context = _contextFactory.CreateDbContext();
            User? User = await context.Users.SingleOrDefaultAsync(u => u.UserId == userIdParam);
            return User;
        }
        public async Task<int> CreateUserAsync(NewUserModel newUserValues)
        {
            using var context = _contextFactory.CreateDbContext();
            var user = await context.Users.SingleOrDefaultAsync(u => u.EmailAddress == newUserValues.ConfirmEmail);
            if (user != null) {
                throw new Exception($"User with email {newUserValues.ConfirmEmail} already exists.");
            }
            var newUser = new User()
            {
                EmailAddress = newUserValues.ConfirmEmail,
                EncryptedPassword = EncryptionHelper.CreatePasswordHash(newUserValues.ConfirmPassword),
                UserType = UserType.Standard
            };
            context.Add(newUser);
            await context.SaveChangesAsync(); //Save Changes updates the ID of the new user

            return newUser.UserId;
        }
    }
}
