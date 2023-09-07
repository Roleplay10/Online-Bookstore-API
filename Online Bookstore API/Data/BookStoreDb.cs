using Microsoft.EntityFrameworkCore;
using Online_Bookstore_API.Data.Entities;

namespace Online_Bookstore_API.Data
{
    public class BookStoreDb : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookStoreDb(DbContextOptions<BookStoreDb> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
