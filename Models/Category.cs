namespace Personal_Finance_Tracker___Enterprise_Edition.Models
{
    public class Category
    {
        public int Id {  get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required string UserId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User? User { get; set; }
    }
}
