using DataBusinessLogic.DTOs.BookDTOs;
using DataBusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Online_Bookstore_API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            List<BookDTO> bookList = _bookService.GetBooks();
            if(bookList is null)
            {
                return NotFound();
            }
            return Ok(bookList);
        }
        [HttpGet("{query}")]
        public IActionResult SearchBooks(string query)
        {
            List<BookDTO> bookList = _bookService.SearchBooks(query);
            if (bookList is null)
            {
                return NotFound();
            }
            return Ok(bookList);
        }
    }
}