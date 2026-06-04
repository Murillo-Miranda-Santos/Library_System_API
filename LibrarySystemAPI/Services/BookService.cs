using LibrarySystemAPI.DTOs.books;
using LibrarySystemAPI.Models;
namespace LibrarySystemAPI.Services;

public class BookService
{
    private readonly LibraryContext _context;

    public BookService(LibraryContext context)
    {
        _context = context;
    }

    public List<BookResponseDto> GetAllBooks()
    {
        List<BookResponseDto> bookResponseDtos = new List<BookResponseDto>();

        foreach (var book in _context.Books)
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

    public BookResponseDto? GetBook(int id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);

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

    public BookResponseDto? PostBook(CreateBookDto createBookDto)
    {
        Book book = new()
        {
            Title = createBookDto.Title
        };

        _context.Books.Add(book);
        _context.SaveChanges();

        BookResponseDto bookResponseDto = new()
        {
            Id = book.Id,
            Title = book.Title,
            Status = "Disponível"
        };

        return bookResponseDto;
    }

    public BookResponseDto? PutBook(int id, UpdateBookDto updatedBook)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return null;

        book.Title = updatedBook.Title;
        _context.SaveChanges();

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

    public BookResponseDto? DeleteBook(int id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);

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
        _context.SaveChanges();

        return bookResponseDto;
    }
}
