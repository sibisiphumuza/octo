using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using octo.Domain.Model;
using octo.Infrastructure.Persistance;
using octo.Presentation.Interface;
using System.Linq.Expressions;

namespace octo.Infrastructure.Repo
{
    public class UserRepo(IdentityContext context, IPasswordHasher<OctoUser> passwordHasher) : IUserRepo
    {
        private readonly IdentityContext _context = context;
        private readonly IPasswordHasher<OctoUser> _passwordHasher = passwordHasher;

        public async Task<OctoUser> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found.");
            }
            return user;
        }
        public async Task<OctoUser> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<OctoUser> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddAsync(OctoUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(OctoUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await GetByUsernameAsync(username);
            if (user == null)
            {
                return false;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success;
        }
        public async Task<IEnumerable<OctoUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<OctoUser, bool>> predicate)
        {
            return await _context.Users.AnyAsync(predicate);
        }
    }
}

