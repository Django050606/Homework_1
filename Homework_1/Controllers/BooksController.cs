using Homework_1.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

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


        [HttpGet]
        [Route("api/books")]
        public ActionResult GetBookByName(string name)//нахождение конкретной книги по имени
        {
            var book = Books.Find(b => b.Name == name);

            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/books/{id}")]
        public ActionResult DeleteBook(int id)//удаление
        {
            var bookToRemove = Books.FirstOrDefault(b => b.Id == id);

            if (bookToRemove != null)
            {
                Books.Remove(bookToRemove);
                return Ok(bookToRemove); // Возвращаем удаленную книгу в теле ответа, если нужно
            }
            else
            {
                return NotFound(); // Возвращаем 404
            }
        }

        [HttpPost]
        [Route("api/books")]
        public ActionResult PostBook([FromBody] Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest("Invalid data. Book data is null.");
            }

            //нифига я умный, просто добавил Count и вуаля, новая айдишка пхахах
            newBook.Id = Books.Count + 1;

            Books.Add(newBook);

            return Created($"/api/books/{newBook.Id}", newBook);//возвращаем 201
        }

        [HttpPut]
        [Route("api/books/{id}")]
        public ActionResult PutBook(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest("Invalid data. Book data is null.");
            }

            var existingBook = Books.FirstOrDefault(b => b.Id == id);

            if (existingBook == null)
            {
                return NotFound(); // Книга с указанным идентификатором не найдена
            }

            
            existingBook.Name = updatedBook.Name; //можем добавить другие поля по необходимости которые можно изменить 

            return Ok(existingBook); // Возвращаем обновленную книгу в теле ответа
        }

    }
}
