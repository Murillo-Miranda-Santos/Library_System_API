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
        public async Task<IActionResult> Get()
        {
            var bookResponseDtos = await _bookService.GetAllBooks();

            return Ok(bookResponseDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bookResponseDto = await _bookService.GetBook(id);

            if (bookResponseDto == null)
                return NotFound("Book not found.");

            return Ok(bookResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBookDto createBookDto)
        {
            var bookResponseDto = await _bookService.PostBook(createBookDto);

            return Ok(bookResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateBookDto updateBookDto, int id)
        {
            var bookResponseDto = await _bookService.PutBook(id, updateBookDto);

            if (bookResponseDto == null)
                return NotFound("Book not found.");

            return Ok(bookResponseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookResponseDto = await _bookService.DeleteBook(id);

            if (bookResponseDto == null)
                return NotFound("Book not found.");

            if (bookResponseDto.Status == "Alugado")
                return BadRequest("Book is currently loaned.");

            return NoContent();
        }
    }
}