using InventorySystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Inventory> Inventories { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<InventoryAccess> InventoryAccesses { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<ItemLike> ItemLikes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Unique constraint: inventory + custom id
            builder.Entity<Item>().HasIndex(i => new { i.InventoryId, i.CustomId }).IsUnique();

            // Concurrency token for Item
            builder.Entity<Item>().Property(i => i.RowVersion).IsRowVersion();

            // Inventory: next sequence default
            builder.Entity<Inventory>().Property(i => i.NextSequence).HasDefaultValue(1);

            // Avoid multiple cascade paths: restrict delete from users to inventories/accesses
            builder.Entity<Inventory>()
                .HasOne(i => i.Owner)
                .WithMany()
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InventoryAccess>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
