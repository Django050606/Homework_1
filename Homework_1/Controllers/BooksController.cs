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
                
                Name = addBookRequest.Name,
                Author = addBookRequest.Author,
                YearOfWriting = addBookRequest.YearOfWriting,
                
            };

            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            return Ok(dbContext.Books); 
        }






        [HttpPut]
        [Route("api/[controller]")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, UpdateBookRequest updateBookRequest)
        {
            var Book=dbContext.Books.Find(id);
            if (Book != null)
            {
                Book.Name= updateBookRequest.Name;
                Book.Author=updateBookRequest.Author;
                Book.YearOfWriting=updateBookRequest.YearOfWriting;
                
                await dbContext.SaveChangesAsync();
                return Ok(Book);
            }
            return NotFound();

        }

    }
}
