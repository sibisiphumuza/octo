using System.ComponentModel;

namespace octo.Presentation.Dtos
{
    public class RegisterDto
    {
        public Guid? Id { get; set; }
        [DisplayName("{ Government Name }")]
        public required string FullName { get; set; }
        [DisplayName("{ Avatar Name }")]
        public required string Username { get; set; }
        [DisplayName("{ snail-mail }")]
        public required string Email { get; set; }
        public required string Password { get; set; }
        [DisplayName("Daytime No.")]
        public required string DayNumber { get; set; }
        [DisplayName("IdentityNumber")]
        public required string IdentityNumber { get; set; }
        [DisplayName("Domicile")]
        public required string Address { get; set; }
        [DisplayName("Current Location")]
        public string? Location { get; set; }
        [DisplayName("Pronoun")]
        public string? Gender { get; set; }
    }
}
