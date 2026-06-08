using LibrarySystemAPI.DTOs.books;
using LibrarySystemAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace LibrarySystemAPI.Services;

public class BookService
{
    private readonly LibraryContext _context;

    public BookService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<BookResponseDto>> GetAllBooks()
    {
        List<BookResponseDto> bookResponseDtos = new List<BookResponseDto>();

        var books = await _context.Books.ToListAsync();

        foreach (var book in books)
        {
            BookResponseDto dto = new()
            {
                Id = book.Id,
                Title = book.Title,
            };

            if (book.IsLoaned == true)
                dto.Status = "Alugado";
            else
                dto.Status = "Disponível";

            bookResponseDtos.Add(dto);
        }

        return bookResponseDtos;
    }

    public async Task<BookResponseDto?> GetBook(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

        if (book == null)
            return null;

        BookResponseDto bookResponseDto = new()
        {
            Id = book.Id,
            Title = book.Title,
        };

        if (book.IsLoaned == true)
            bookResponseDto.Status = "Alugado";
        else
            bookResponseDto.Status = "Disponível";

        return bookResponseDto;
    }

    public async Task<BookResponseDto?> PostBook(CreateBookDto createBookDto)
    {
        Book book = new()
        {
            Title = createBookDto.Title
        };

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        BookResponseDto bookResponseDto = new()
        {
            Id = book.Id,
            Title = book.Title,
            Status = "Disponível"
        };

        return bookResponseDto;
    }

    public async Task<BookResponseDto?> PutBook(int id, UpdateBookDto updatedBook)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

        if (book == null)
            return null;

        book.Title = updatedBook.Title;
        await _context.SaveChangesAsync();

        BookResponseDto bookResponseDto = new()
        {
            Id = book.Id,
            Title = book.Title
        };

        if (book.IsLoaned == true)
            bookResponseDto.Status = "Alugado";
        else
            bookResponseDto.Status = "Disponível";

        return bookResponseDto;
    }

    public async Task<BookResponseDto?> DeleteBook(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

        if (book == null)
            return null;

        BookResponseDto bookResponseDto = new()
        {
            Id = book.Id,
            Title = book.Title,
        };

        if (book.IsLoaned == true)
            bookResponseDto.Status = "Alugado";
        else
            bookResponseDto.Status = "Disponível";

        if (bookResponseDto.Status == "Alugado")
            return bookResponseDto;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return bookResponseDto;
    }
}
