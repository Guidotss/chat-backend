using Chat_backend.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Chat_backend.Frameworks___Drivers.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChatUser> ChatUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
