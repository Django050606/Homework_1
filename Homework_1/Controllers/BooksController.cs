using Homework_1.Data;
using Homework_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Homework_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksApiDbContext dbContext;

        public BooksController(BooksApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(dbContext.Books.ToList());
        }


        [HttpPost]
        public IActionResult PostBook(AddBookRequest addBookRequest)
        {
            if (addBookRequest == null)
            {
                return BadRequest("Invalid data. Book data is null.");
            }

            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Name = addBookRequest.Name,
                
            };

            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }






        [HttpPut("{id}")]
        public ActionResult PutBook(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest("Invalid data. Book data is null.");
            }

            var existingBook = dbContext.Books.Find(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Name = updatedBook.Name;

            dbContext.SaveChanges();

            return Ok(existingBook);
        }



        [HttpDelete("{id}")]
        public ActionResult DeleteBookById(int id)
        {
            var bookToRemove = dbContext.Books.Find(id);

            if (bookToRemove != null)
            {
                dbContext.Books.Remove(bookToRemove);
                dbContext.SaveChanges();
                return Ok(bookToRemove);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = dbContext.Books.Find(id);

            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
