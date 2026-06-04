using LibrarySystemAPI.DTOs.books;
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
            var bookResponseDtos = _bookService.GetAllBooks();

            return Ok(bookResponseDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var bookResponseDto = _bookService.GetBook(id);

            if (bookResponseDto == null)
                return NotFound("Book not found.");

            return Ok(bookResponseDto);
        }

        [HttpPost]
        public IActionResult Post(CreateBookDto createBookDto)
        {
            var bookResponseDto = _bookService.PostBook(createBookDto);

            return Ok(bookResponseDto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateBookDto updateBookDto, int id)
        {
            var bookResponseDto = _bookService.PutBook(id, updateBookDto);

            if (bookResponseDto == null)
                return NotFound("Book not found.");

            return Ok(bookResponseDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookResponseDto = _bookService.DeleteBook(id);

            if (bookResponseDto == null)
                return NotFound("Book not found.");

            if (bookResponseDto.Status == "Alugado")
                return BadRequest("Book is currently loaned.");

            return NoContent();
        }
    }
}