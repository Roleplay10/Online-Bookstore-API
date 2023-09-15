using DataAccessLayer.Data;
using DataBusinessLogic.Builders;
using DataBusinessLogic.DTOs.BookDTOs;

namespace DataBusinessLogic.Services
{
    public interface IBookService
    {
        List<BookDTO> GetBooks();
        List<BookDTO> SearchBooks(string searchQuery);
    }
    public class BookService : IBookService
    {
        private readonly BookStoreDb _db;
        private readonly IBookDTOBuilder _bookDTOBuilder;
        public BookService(BookStoreDb db, IBookDTOBuilder bookDTOBuilder)
        {
            _db = db;
            _bookDTOBuilder = bookDTOBuilder;
        }
        public List<BookDTO> GetBooks()
        {
            IEnumerable<BookDTO> bookDTOs = _db.Books
                .Select(x => _bookDTOBuilder.Build(x));
            return bookDTOs.ToList();
        }
        public List<BookDTO> SearchBooks(string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            List<BookDTO> matchingBooks = new List<BookDTO>();

            foreach (var book in _db.Books)
            {
                string[] _genre = book.Genre.ToLower().Split(new[] { ',' }, StringSplitOptions.TrimEntries);
                string[] _author = book.Author.ToLower().Split(new[] {' '},StringSplitOptions.TrimEntries);
                string[] _title = book.Author.ToLower().Split(new[] { ' ' }, StringSplitOptions.TrimEntries);

                if (_genre.Any(genre => genre.Contains(searchQuery)) ||
                    _author.Any(author => author.Contains(searchQuery)) ||
                    _title.Any(title => title.Contains(searchQuery)) ||
                    book.Author.ToLower().Contains(searchQuery) ||
                    book.Title.ToLower().Contains(searchQuery) ||
                    book.Genre.ToLower().Contains(searchQuery))
                {
                    matchingBooks.Add(_bookDTOBuilder.Build(book));
                }
            }

            return matchingBooks;
        }


    }
}
