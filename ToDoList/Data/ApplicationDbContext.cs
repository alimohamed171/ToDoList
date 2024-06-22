using Microsoft.EntityFrameworkCore;
using ToDoList.Data.UserData;

namespace ToDoList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options) 
        {
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);

                // One-to-many relationship between User and Notes
                entity.HasMany(e => e.Notes)
                      .WithOne(n => n.User)
                      .HasForeignKey(n => n.UserId);
            });

            // Configuring the Note entity
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Notes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.IsPublic).IsRequired();

                //// One-to-many relationship between Note and User
                //entity.HasOne(n => n.User)
                //      .WithMany(u => u.Notes)
                //      .HasForeignKey(n => n.UserId);
            });





            //modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Id);
            //modelBuilder.Entity<Note>().ToTable("Notes").HasKey(n => n.Id);

            //modelBuilder.Entity<Note>()
            //    .HasOne(n => n.User)
            //    .WithMany(u => u.Notes)
            //    .HasForeignKey(n => n.UserId);

        }
    }
}


 //// Configuring the User entity
            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.ToTable("Users");
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            //    entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
            //    entity.Property(e => e.Password).IsRequired().HasMaxLength(255);

            //    // One-to-many relationship between User and Notes
            //    entity.HasMany(e => e.Notes)
            //          .WithOne(n => n.User)
            //          .HasForeignKey(n => n.UserId);
            //});

            //// Configuring the Note entity
            //modelBuilder.Entity<Note>(entity =>
            //{
            //    entity.ToTable("Notes");
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            //    entity.Property(e => e.Content).IsRequired();
            //    entity.Property(e => e.IsPublic).IsRequired();

            //    // One-to-many relationship between Note and User
            //    entity.HasOne(n => n.User)
            //          .WithMany(u => u.Notes)
            //          .HasForeignKey(n => n.UserId);
            //});