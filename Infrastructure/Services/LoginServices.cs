using Microsoft.AspNetCore.Identity;
using octo.Application.Interface;
using octo.Domain.Model;
using octo.Presentation.Dtos;
using octo.Presentation.Interface;

namespace octo.Infrastructure.Services
{
    public class LoginServices(IUserRepo userRepository, IPasswordHasher<OctoUser> passwordHasher, IEmailService emailService) : ILoginService
    {
        private readonly IUserRepo _userRepository = userRepository;
        private readonly IPasswordHasher<OctoUser> _passwordHasher = passwordHasher;
        private readonly IEmailService _emailService = emailService;

        public async Task<OctoUser> LoginAsync(string username, string password)
        {
            if (await _userRepository.ValidateCredentialsAsync(username, password))
            {
                return await _userRepository.GetByUsernameAsync(username);
            }
            throw new Exception("Invalid username or password");
        }
        public async Task ChangePasswordAsync(int userId, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new Exception("User not found");
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            await _userRepository.UpdateAsync(user);
        }
        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email) ?? throw new Exception("User not found");

            // Generate a password reset token and send an email.
            var resetToken = GenerateResetToken();
            //await _emailService.SendResetPasswordEmailAsync(user.Email, resetToken);
        }
        public async Task ResetPasswordAsync(int userId, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            await _userRepository.UpdateAsync(user);
        }
        private static string GenerateResetToken()
        {
            // Implement token generation logic here.
            return "reset-token"; // Placeholder
        }
        public async Task RegisterUserAsync(RegisterDto registerDto)
        {
            if (await _userRepository.AnyAsync(u => u.Fullname == registerDto.FullName))
            {
                throw new KeyNotFoundException($"User with fullname {registerDto.FullName} already exists.");
            }

            if (!int.TryParse(registerDto.IdentityNumber, out int identityNumberInt))
            {
                throw new ArgumentException("IdentityNumber is not a valid integer.");
            }

            byte[] identityNumberBytes = BitConverter.GetBytes(identityNumberInt);

            var user = new OctoUser
            {
                Fullname = registerDto.FullName,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(null, registerDto.Password),                             
            };

            await _userRepository.AddAsync(user);
            await SendWelcomeEmail(registerDto.Email, registerDto.FullName);
        }
        private async Task SendWelcomeEmail(string email, string fullName)
        {
            string subject = "Hello, & Welcome!";
            string body = GenerateWelcomeEmailBody(fullName);
            await _emailService.SendEmailAsync(email, subject, body);
        }
        private static string GenerateWelcomeEmailBody(string fullName)
        {
            return $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #F5F5DC; /* Beige background for a welcoming feel */
                    color: #4B5320; /* Army brown text color */
                }}
                .container {{
                    padding: 20px;
                    border: 1px solid #4B5320;
                    border-radius: 10px;
                    background-color: #F0E68C; /* Olive green background */
                    max-width: 600px;
                    margin: auto;
                }}
                .header {{
                    color: #4B5320;
                    font-size: 24px;
                    font-weight: bold;
                }}
                .content {{
                    font-size: 16px;
                    line-height: 1.5;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 14px;
                    color: #4B5320;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    Hello, & Welcome! 👋
                </div>
                <div class='content'>
                    Hey {fullName},<br /><br />
                    Welcome to our community! We're thrilled to have you here. 🎉<br /><br />
                    We hope you find everything you're looking for and more. If you have any questions, feel free to reach out to our support team.<br /><br />
                    Best regards,<br />
                    The Team
                </div>
                <div class='footer'>
                    Stay Connected! 🌐<br />
                    Follow us on social media for the latest updates and news.
                </div>
            </div>
        </body>
        </html>
        ";
        }
    }

}
