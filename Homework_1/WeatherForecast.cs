using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = new List<object>
        {
            new { id = 1, name = "test1" },
            new { id = 2, name = "test2" }
        };
        return Ok(books);
    }
}
