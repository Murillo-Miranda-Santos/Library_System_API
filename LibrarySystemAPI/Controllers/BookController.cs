using LibrarySystemAPI.Models;
using LibrarySystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemAPI.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookService.GetAllBooks());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.GetBook(id);

            if (book == null)
                return NotFound("Book not found.");

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post(Book book)
        {
            var created = _bookService.PostBook(book);

            if (created == null)
                return BadRequest("Invalid book data.");

            return Ok(created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Book updatedBook)
        {
            var book = _bookService.PutBook(id, updatedBook);

            if (book == null)
                return NotFound("Book not found.");

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _bookService.DeleteBook(id);

            if (result == null)
                return NotFound("Book not found.");

            if (result == false)
                return BadRequest("Book is currently loaned.");

            return NoContent();
        }
    }
}