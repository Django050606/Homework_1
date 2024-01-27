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
        public ActionResult<IEnumerable<Book>> GetBooks() => Ok(dbContext.Books.ToList());


        [HttpPost]
        public IActionResult PostBook(AddBookRequest addBookRequest)
        {
            if (addBookRequest == null)
            {
                return BadRequest("Invalid data. Book data is null.");
            }

            var book = new Book()
            {
                
                Name = addBookRequest.Name,
                Author = addBookRequest.Author,
                YearOfWriting = addBookRequest.YearOfWriting,
                
            };

            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            return Ok(dbContext.Books); 
        }






        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, UpdateBookRequest updateBookRequest)
        {
            var book = dbContext.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            book.Name = updateBookRequest.Name;
            book.Author = updateBookRequest.Author;
            book.YearOfWriting = updateBookRequest.YearOfWriting;

            await dbContext.SaveChangesAsync();

            return Ok(book);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBook([FromRoute]Guid id)
        {
            var book=await dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBook([FromRoute]Guid id)
        {
            var book=await dbContext.Books.FindAsync(id);
            if(book != null)
            {
                dbContext.Books.Remove(book);
                dbContext.SaveChanges();
                return Ok("This "+book+" is deleted!");
            }
            return NotFound();
        }
    }
}
