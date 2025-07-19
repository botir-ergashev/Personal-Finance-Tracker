using System.ComponentModel.DataAnnotations;

namespace Personal_Finance_Tracker___Enterprise_Edition.DTO
{
    public class SignUpDTO
    {

        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
