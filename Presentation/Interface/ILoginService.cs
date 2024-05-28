using octo.Domain.Model;
using octo.Presentation.Dtos;

namespace octo.Application.Interface
{
    public interface ILoginService
    {
        Task RegisterUserAsync(RegisterDto registerDto);
        Task<OctoUser> LoginAsync(string username, string password);
        Task ChangePasswordAsync(int userId, string newPassword);
        Task ForgotPasswordAsync(string email);
        Task ResetPasswordAsync(int userId, string newPassword);
    }
}
