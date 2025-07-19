using System.ComponentModel.DataAnnotations;

namespace Personal_Finance_Tracker___Enterprise_Edition.DTO
{
    public class LoginDTO
    {
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
