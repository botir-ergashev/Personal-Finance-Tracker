using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Personal_Finance_Tracker___Enterprise_Edition.Enumerations;
using System.Configuration;
using System.Globalization;
using System.Reflection.Metadata;

namespace Personal_Finance_Tracker___Enterprise_Edition.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        private readonly IConfiguration _config;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
            Database.EnsureDeleted();
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User testUser1 = new User { Id = "51cd0541-b627-40ec-890e-5cbf8b5847cc", NormalizedUserName= "TEST1", UserName = "Test1", Email = "test.test@test.te", Role = "Customer", IsActive = true, PasswordHash = "fake" };
            User testUser2 = new User { Id= "1caccb3c-29c2-455f-bd43-16c8a4bd9190", NormalizedUserName = "TEST2", UserName = "Test2", Email = "test.test@test.te", Role = "Customer", IsActive = true, PasswordHash = "fake" };
            User testUser3 = new User { Id= "093a4a05-e238-41e6-a3ab-de51027e7d17", NormalizedUserName = "TEST3", UserName = "Test3", Email = "test.test@test.te", Role = "Admin", IsActive = true, PasswordHash = "fake" };

            Category testCategory1 = new Category { Id = 1, Name = "Transport", Color = "#ff00ff", UserId = testUser1.Id, IsDeleted=false };
            Category testCategory2 = new Category { Id = 2, Name = "Ovqatlanish", Color = "#00ff00", UserId = testUser1.Id, IsDeleted = false };
            Category testCategory3 = new Category { Id = 3, Name = "Ovqatlanish", Color = "#00ff00", UserId = testUser3.Id, IsDeleted = false };

            Transaction testTransaction1 = new Transaction { Id = 1, Amount = 1000, UserId = testUser1.Id, CategoryId=1, IsDeleted = false, CreatedAt = DateTime.ParseExact("01-07-2025 12:05:56", "dd-MM-yyyy HH:mm:ss", null), Type = TransactionType.None };
            Transaction testTransaction2 = new Transaction { Id = 2, Amount = 1001, UserId = testUser1.Id, Note="Test note", CategoryId = 2, IsDeleted=false, CreatedAt = DateTime.ParseExact("01-07-2025 12:05:56", "dd-MM-yyyy HH:mm:ss", null), Type = TransactionType.None };
            Transaction testTransaction3 = new Transaction { Id = 3, Amount = 1002, UserId = testUser2.Id, IsDeleted = false, CreatedAt = DateTime.ParseExact("01-07-2025 12:05:56", "dd-MM-yyyy HH:mm:ss", null), Type = TransactionType.None };
            Transaction testTransaction4 = new Transaction { Id = 4, Amount = 1003, UserId = testUser3.Id, CategoryId = 3, IsDeleted = false, CreatedAt = DateTime.ParseExact("01-07-2025 12:05:56", "dd-MM-yyyy HH:mm:ss", null), Type=TransactionType.None };



            modelBuilder.Entity<User>().HasData(testUser1, testUser2, testUser3);
            modelBuilder.Entity<Category>().HasData(testCategory1, testCategory2, testCategory3);
            modelBuilder.Entity<Transaction>().HasData(testTransaction1, testTransaction2, testTransaction3, testTransaction4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
