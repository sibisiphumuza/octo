using octo.Domain.Model;
using System.Linq.Expressions;

namespace octo.Presentation.Interface
{
    public interface IUserRepo
    {
        Task<OctoUser> GetByIdAsync(int id);
        Task<OctoUser> GetByUsernameAsync(string username);
        Task<OctoUser> GetByEmailAsync(string email);
        Task AddAsync(OctoUser user);
        Task UpdateAsync(OctoUser user);
        Task RemoveAsync(int id);
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<IEnumerable<OctoUser>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<OctoUser, bool>> predicate);
    }
}
