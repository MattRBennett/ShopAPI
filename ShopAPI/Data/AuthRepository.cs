using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShopAPI.Migrations;
using ShopAPI.Models;
using User = ShopAPI.Models.User;

namespace ShopAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<int>> Login(string username, string password)
        {
            var response = new ServiceResponse<int>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

            if (user != null) 
            { 
                if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Failed to log in, username/password is incorrect.";
                }
                else
                {
                    
                    response.Data = user.Id;
                    response.Success = true;
                    response.Message = "Successfully logged in.";
                }
            } 
            else
            {
                response.Success = false;
                response.Message = "User not found, please register first before logging in.";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            bool DoesUserExist = await UserExists(user.Username);
            
            if (!DoesUserExist)
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passswordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passswordSalt;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "New user has successfully been registered.";
                response.Data = user.Id;
            }
            else
            {
                response.Success = false;
                response.Message = "User already exists.";
            }

            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
