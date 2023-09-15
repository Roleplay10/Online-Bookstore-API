using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Data.Entities
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Book> ShoppingCartItems { get; set; }

        public ShoppingCart()
        {
            ShoppingCartItems = new List<Book>();
        }
    }
}
