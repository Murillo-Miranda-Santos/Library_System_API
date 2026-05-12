using LibrarySystemAPI.Models;
namespace LibrarySystemAPI.Services;

public class LoanService
{
    private readonly LibraryContext _context;

    public LoanService(LibraryContext context)
    {
        _context = context;
    }

    public List<Loan> GetAllLoans()
    {
        return _context.Loans.ToList();
    }

    public Loan? GetLoan(int id)
    {
        return _context.Loans.FirstOrDefault(x => x.Id == id);
    }

    public bool? PostLoan(Loan loan)
    {
        var exUser = _context.Users.Any(x => x.Id == loan.UserId);
        var exBook = _context.Books.Any(x => x.Id == loan.BookId);

        if (!exUser || !exBook)
            return null;

        var book = _context.Books.FirstOrDefault(x => x.Id == loan.BookId);

        if (book.IsLoaned) 
            return false;

        loan.Id = _context.Loans.Any() ? _context.Loans.Max(x => x.Id) + 1 : 1;
        loan.LoanDate = DateTime.Now;

        _context.Loans.Add(loan);

        book.IsLoaned = true;
        _context.SaveChanges();

        return true;
    }

    public bool? PostReturn(int id)
    {
        var loan = _context.Loans.FirstOrDefault(x => x.Id == id);

        if (loan == null)
            return null;

        if (loan.ReturnDate != null)
            return false;

        var book = _context.Books.FirstOrDefault(x => x.Id == loan.BookId);

        if (book == null)
            return null;

        loan.ReturnDate = DateTime.Now;
        book.IsLoaned = false;

        _context.SaveChanges();

        return true;
    }
}
