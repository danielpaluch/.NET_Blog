using Microsoft.EntityFrameworkCore;
namespace BlogAPI.Entities
{
        public class BlogDbContext : DbContext
        {
            private string _connectionString = "Server=DESKTOP-E59MHE2;Database=BlogDb;Trusted_Connection=True";
            // DESKTOP-T1HVHEK komp
            // DESKTOP-E59MHE2 lap
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Blog>()
                    .Property(r => r.Title)
                    .IsRequired()
                    .HasMaxLength(25);

                modelBuilder.Entity<Category>()
                    .Property(r => r.Name)
                    .IsRequired();
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

        }
    
}
