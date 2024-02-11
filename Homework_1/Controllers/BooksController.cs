using Homework_1.Data;
using Homework_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Homework_1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksAPIDbContext dbContext;

        public BooksController(BooksAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


      



        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks() => Ok(dbContext.Books.ToList());


        [HttpPost]
        public IActionResult PostBooks(List<AddBookRequest> addBookRequests)
        {
            if (addBookRequests == null || !addBookRequests.Any())
            {
                return BadRequest("Invalid data. Book data is null or empty.");
            }

            var books = addBookRequests.Select(addBookRequest => new Book
            {
                Name = addBookRequest.Name,
                Author = addBookRequest.Author,
                YearOfWriting = addBookRequest.YearOfWriting,
            }).ToList();

            dbContext.Books.AddRange(books);
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


        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
        public ActionResult<IEnumerable<Book>> GetBooksBySearch(
    [FromQuery] int year,
    [FromQuery] string author)
 
        {
            IQueryable<Book> query = dbContext.Books;

            // Filter by Author
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(book => book.Author == author);
            }

            // Filter by Year
            if (year > 0)
            {
                query = query.Where(book => book.YearOfWriting == year);
            }

            

            var result = query.ToList();
            return Ok(result);
        }

        public enum SortOrderEnum
        {
            Ascending,
            Descending
        }




    }
}
