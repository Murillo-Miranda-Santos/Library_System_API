using LibrarySystemAPI.Models;
namespace LibrarySystemAPI.Services;

public class LoanService
{
    public List<Loan> GetAllLoans()
    {
        var loans = DataStore.loans;

        return loans;
    }

    public Loan? GetLoan(int id)
    {
        var loan = DataStore.loans.FirstOrDefault(x => x.Id == id);

        return loan;
    }

    public bool? PostLoan(Loan loan)
    {
        var exUser = DataStore.users.Any(x => x.Id == loan.UserId);
        var exBook = DataStore.books.Any(x => x.Id == loan.BookId);

        if (!exUser || !exBook)
            return null;

        var book = DataStore.books.FirstOrDefault(x => x.Id == loan.BookId);

        if (book.IsLoaned) 
            return false;

        loan.Id = DataStore.loans.Any() ? DataStore.loans.Max(x => x.Id) + 1 : 1;
        loan.LoanDate = DateTime.Now;

        DataStore.loans.Add(loan);

        book.IsLoaned = true;

        return true;
    }

    public bool? PostReturn(int id)
    {
        var loan = DataStore.loans.FirstOrDefault(x => x.Id == id);

        if (loan == null)
            return null;

        if (loan.ReturnDate != null)
            return false;

        var book = DataStore.books.FirstOrDefault(x => x.Id == loan.BookId);

        if (book == null)
            return null;

        loan.ReturnDate = DateTime.Now;
        book.IsLoaned = false;

        return true;
    }
}
