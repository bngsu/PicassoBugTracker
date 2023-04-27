using Microsoft.EntityFrameworkCore;
using PBugTracker.Models;

namespace PBugTracker
{
    public class BugTrackerDbContext : DbContext
    {
        public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=ANK1-YZLMORT-08\SQLEXPRESS;Database=BugTrackerDb;User Id=sa;Password=sa;");
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BugTracker;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public DbSet<Bug> Bug { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bug>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Project>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Comment>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

        }
    }
}

