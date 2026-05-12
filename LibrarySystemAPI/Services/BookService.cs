using LibrarySystemAPI.Models;
namespace LibrarySystemAPI.Services;

public class BookService
{
    private readonly LibraryContext _context;

    public BookService(LibraryContext context)
    {
        _context = context;
    }

    public List<Book> GetAllBooks()
    {
        return _context.Books.ToList();
    }

    public Book? GetBook(int id)
    {
        return _context.Books.FirstOrDefault(x => x.Id == id);
    }

    public Book? PostBook(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Title))
            return null;

        _context.Books.Add(book);
        _context.SaveChanges();

        return book;
    }

    public Book? PutBook(int id, Book updatedBook)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return null;

        book.Title = updatedBook.Title;
        _context.SaveChanges();

        return book;
    }

    public bool? DeleteBook(int id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return null;

        var isLoaned = _context.Loans.Any(x => x.BookId == id && x.ReturnDate == null); 
        
        if (isLoaned == true) 
            return false;

        _context.Books.Remove(book);
        _context.SaveChanges();

        return true;
    }
}
