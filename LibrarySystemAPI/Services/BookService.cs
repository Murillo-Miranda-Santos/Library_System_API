using LibrarySystemAPI.Models;
namespace LibrarySystemAPI.Services;

public class BookService
{
    public List<Book>? GetAllBooks()
    {
        var books = DataStore.books;

        return books;
    }

    public Book? GetBook(int id)
    {
        return DataStore.books.FirstOrDefault(x => x.Id == id);
    }

    public Book? PostBook(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Title))
            return null;

        book.Id = DataStore.books.Any() ? DataStore.books.Max(x => x.Id) + 1 : 1;

        DataStore.books.Add(book);

        return book;
    }

    public Book? PutBook(int id, Book updatedBook)
    {
        var book = DataStore.books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return null;

        book.Title = updatedBook.Title;

        return book;
    }

    public bool? DeleteBook(int id)
    {
        var book = DataStore.books.FirstOrDefault(x => x.Id == id);

        if (book == null)
            return null;

        var isLoaned = DataStore.loans.Any(x => x.BookId == id && x.ReturnDate == null); 
        
        if (isLoaned == true) 
            return false;

        DataStore.books.Remove(book);

        return true;
    }
}
