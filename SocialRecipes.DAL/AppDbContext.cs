using Microsoft.EntityFrameworkCore;
using SocialRecipes.DAL.Models;

namespace SocialRecipes.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set default string to varchar(255)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                property.SetColumnType("varchar(255)");
            }

            // Set default DateTime to datetime
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(DateTime)))
            {
                property.SetColumnType("datetime");
            }

            // Configure Message entity relationships
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(m => m.Sender)
                    .WithMany(u => u.SentMessages)
                    .HasForeignKey(m => m.SenderId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

                entity.HasOne(m => m.Receiver)
                    .WithMany(u => u.ReceivedMessages)
                    .HasForeignKey(m => m.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
            });

            // Configure Follower composite key and relationships
            modelBuilder.Entity<Follower>()
                .HasKey(f => new { f.FollowedUserId, f.FollowingUserId });

            modelBuilder.Entity<Follower>()
                .HasOne(f => f.FollowedUser)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FollowedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follower>()
                .HasOne(f => f.FollowingUser)
                .WithMany()
                .HasForeignKey(f => f.FollowingUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Recipe.Image as longblob
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Image)
                .HasColumnType("longblob");

            // Add unique index for User.Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Apply snake_case naming convention
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(ToSnakeCase(entity.GetTableName()));

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.Name));
                }
            }

            // Utility function to convert to snake_case
            string ToSnakeCase(string input)
            {
                return string.Concat(input.Select((x, i) =>
                    i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
