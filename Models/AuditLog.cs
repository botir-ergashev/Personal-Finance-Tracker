namespace Personal_Finance_Tracker___Enterprise_Edition.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public required string UserId {  get; set; }
        public required string Action {  get; set; }
        public required string EntityName { get; set; }
        public int EntityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public virtual User? User { get; set; }
    }
}
