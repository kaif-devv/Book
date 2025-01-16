using BookModel = Books.Models;
using Microsoft.AspNetCore.Mvc;
using Books.Services;
namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

// Get all books
        [HttpGet]
        [Route("GetBooks")]
        public List<BookModel.Books> GetBooks()
        {
            return _booksService.GetAllBooks();
        }

// Get book by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel.Books>> GetBooks(int id)
        {
            var book = await _booksService.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            return Ok(book);
        }

// Update the book by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooks(int id, BookModel.BookUpdateDto bookUpdate)
        {
            if (bookUpdate == null)
            {
                return BadRequest("Book data is required");
            }

            var book = await _booksService.UpdateBook(id, bookUpdate);
            if (!book.success)
            {
                return NotFound("Book not found");
            }

            return Ok("Book Updated Successfully");
        }

// Add a new book
        [HttpPost]
        [Route("AddBooks")]
        public string PostBooks(BookModel.Books books)
        {
            return _booksService.AddBook(books);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            return await _booksService.DeleteBook(id) ? Ok() : NotFound("Book not found");
        }
    }
}
