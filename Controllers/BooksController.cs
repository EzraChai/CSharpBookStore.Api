using BookStore.Api.Data;
using BookStore.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(BookStoreContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await dbContext.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await dbContext.Books.FindAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBook),
                new { id = book.Id },
                book
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await dbContext.Books.FindAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
