using Homework_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_1.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> Books = new List<Book>
    {
        new Book { Id = 1, Name = "test1" },
        new Book { Id = 2, Name = "test2" }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(Books);
        }
    }
}
