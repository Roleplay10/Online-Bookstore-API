using DataAccessLayer.Data.Entities;
using DataBusinessLogic.DTOs.BookDTOs;

namespace DataBusinessLogic.Builders
{
    public interface IBookDTOBuilder
    {
        BookDTO Build(Book b);
        Book BookBuild(BookDTO book);
    }
    public class BookDTOBuilder : IBookDTOBuilder
    {
        public BookDTO Build(Book b)
        {
            return new BookDTO
            {
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                PublicationYear = b.PublicationYear,
                Genre = b.Genre
            };
        }
        public Book BookBuild(BookDTO book)
        {
            return new Book
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                PublicationYear = book.PublicationYear,
                Genre = book.Genre
            };
        }

    }
}
