using DataAccessLayer.Data;
using DataAccessLayer.Data.Entities;
using DataBusinessLogic.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataBusinessLogic.Services
{
    public interface ICartService
    {
        int getCartId(int id);
        Task<bool> AddBooks(int user_id,int book_id);
        IEnumerable<Book> viewCart(int user_id);
        Task<bool> DeleteBook(int user_id,int book_id);
    }
    public class CartService : ICartService
    {
        private readonly BookStoreDb _db;
        private readonly IBookDTOBuilder _bookDTOBuilder;

        public CartService(BookStoreDb db,IBookDTOBuilder bookDTOBuilder)
        {
            _db = db;
            _bookDTOBuilder = bookDTOBuilder;
        }
        public int getCartId(int id)
        {
            var user = _db.Users
                .Where(u => u.UserId == id)
                .Include(u => u.ShoppingCart)
                .SingleOrDefault();
            ShoppingCart cart = user.ShoppingCart;
            
            return cart.UserId;
        }
        public async Task<bool> AddBooks(int user_id, int book_id)
        {
            var book = _db.Books
                .FirstOrDefault(u => u.BookId == book_id);
            var user = _db.Users
                .Include (u => u.ShoppingCart)
                .FirstOrDefault(u => u.UserId == user_id);
            if(book is null || user is null)
            {
                return false;
            }
            user.ShoppingCart.ShoppingCartItems.Add(book);
            await _db.SaveChangesAsync();
            return true;
        }
        public IEnumerable<Book> viewCart(int user_id)
        {
            var user = _db.Users
                .Include(u => u.ShoppingCart)
                .ThenInclude(cart => cart.ShoppingCartItems)
                .FirstOrDefault(u => u.UserId == user_id);
            if(user is null)
            {
                return null;
            }
            var items_list = user.ShoppingCart.ShoppingCartItems.ToList();
            return items_list;
        }
        public async Task<bool> DeleteBook(int user_id, int book_id)
        {
            var user = _db.Users
                .Include(u => u.ShoppingCart)
                .ThenInclude(cart => cart.ShoppingCartItems)
                .FirstOrDefault(u => u.UserId == user_id);
            var book = _db.Books
                .FirstOrDefault(u => u.BookId == book_id);
            if (user is null || book is null)
            {
                return false;
            }
            user.ShoppingCart.ShoppingCartItems.Remove(book);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
