using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookModel = Books.Models.Books;
using BookUpdateDto = Books.Models.BookUpdateDto;
namespace Books.Services
{
    public interface IBooksService
    {
        List<BookModel> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<(bool success, BookModel book)> UpdateBook(int id, BookUpdateDto book);
        string AddBook(BookModel book);
        Task<bool> DeleteBook(int id);
        bool BookExists(int id);
    }

    public class BooksService : IBooksService
    {
        private readonly BooksContext _booksContext;

        public BooksService(BooksContext context)
        {
            _booksContext = context;
        }

        public List<BookModel> GetAllBooks()
        {
            return _booksContext.Books.ToList();
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _booksContext.Books.FindAsync(id);
        }

        // Update the book
        public async Task<(bool success, BookModel book)> UpdateBook(int id, BookUpdateDto book)
        {
            var existingBook = await _booksContext.Books.FindAsync(id);
            if (existingBook == null)
                return (false, null);
            if (book.Name != null)
                existingBook.Name = book.Name;
            if (book.Author != null)
                existingBook.Author = book.Author;

            try
            {
                await _booksContext.SaveChangesAsync();
                return (true, existingBook);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                    return (false, null);
                throw;
            }
        }

        // Create a new book
        public string AddBook(BookModel book)
        {
            _booksContext.Books.Add(book);
            _booksContext.SaveChanges();
            return "Record Added Successfully";
        }

        // Delete a book
        public async Task<bool> DeleteBook(int id)
        {
            var book = await _booksContext.Books.FindAsync(id);
            if (book == null)
                return false;

            _booksContext.Books.Remove(book);
            await _booksContext.SaveChangesAsync();
            return true;
        }

        public bool BookExists(int id)
        {
            return _booksContext.Books.Any(e => e.Id == id);
        }
    }
}