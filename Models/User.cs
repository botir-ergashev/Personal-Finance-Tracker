using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Personal_Finance_Tracker___Enterprise_Edition.Models
{
    public class User : IdentityUser
    {
        [StringLength(50)]
        [MaxLength(50)]
        public override string? UserName { get; set; }
        public override string? Email { get; set; }
        public override string? PasswordHash { get; set; }
        public required string Role { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<Transaction> Transactions { get; set; } = [];
        public virtual IList<Category> Categories { get; set; } = [];
        public virtual IList<AuditLog> AuditLogs { get; set; } = [];
    }
}
