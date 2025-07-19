using Personal_Finance_Tracker___Enterprise_Edition.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal_Finance_Tracker___Enterprise_Edition.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public int CategoryId { get; set; }
        public required string UserId {  get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual User? User {  get; set; }
    }
}
