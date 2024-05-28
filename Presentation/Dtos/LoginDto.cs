using System.ComponentModel;

namespace octo.Presentation.Dtos
{
    public class LoginDto
    {
        [DisplayName("Avatar Name")]
        public required string Username { get; set; }
        [DisplayName("Snail-mail")]
        public required string Email { get; set; }      
        [DisplayName("Encrypt/Lock { Passward }")]
        public required string Password { get; set; }

    }
}
