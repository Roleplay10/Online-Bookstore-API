using DataAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class BookStoreDb : DbContext
    {
        public BookStoreDb(DbContextOptions<BookStoreDb> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDb).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.ShoppingCart)
                .WithOne(sc => sc.User)
                .HasForeignKey<ShoppingCart>(sc => sc.UserId);
            
        }
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
