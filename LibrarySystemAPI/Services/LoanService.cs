using LibrarySystemAPI.Common;
using LibrarySystemAPI.DTOs.books;
using LibrarySystemAPI.DTOs.loans;
using LibrarySystemAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace LibrarySystemAPI.Services;

public class LoanService
{
    private readonly LibraryContext _context;

    public LoanService(LibraryContext context)
    {
        _context = context;
    }

    public List<LoanResponseDto> GetAllLoans()
    {
        List<LoanResponseDto> loanResponseDtos = new List<LoanResponseDto>();

        var loans = _context.Loans.Include(x => x.User).Include(x => x.Book).ToList();

        foreach (var loan in loans)
        {
            LoanResponseDto loanResponseDto = new()
            {
                Id = loan.Id,
                UserName = loan.User.Name,
                BookTitle = loan.Book.Title,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            };

            if (loan.ReturnDate == null)
                loanResponseDto.Status = "Ativo";
            else
                loanResponseDto.Status = "Devolvido";

            loanResponseDtos.Add(loanResponseDto);
        }

        return loanResponseDtos;
    }

    public LoanResponseDto? GetLoan(int id)
    {
        var loan = _context.Loans.Include(x => x.User).Include(x => x.Book).FirstOrDefault(x => x.Id == id);

        if (loan == null)
            return null;

        LoanResponseDto loanResponseDto = new()
        {
            Id = loan.Id,
            UserName = loan.User.Name,
            BookTitle = loan.Book.Title,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate
        };

        if (loanResponseDto.ReturnDate == null)
            loanResponseDto.Status = "Ativo";
        else
            loanResponseDto.Status = "Devolvido";

        return loanResponseDto;
    }
    public ServiceResult<LoanResponseDto?> PostLoan(CreateLoanDto createLoanDto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == createLoanDto.UserId);
        var book = _context.Books.FirstOrDefault(x => x.Id == createLoanDto.BookId);

        if (user == null || book == null)
        {
            return new ServiceResult<LoanResponseDto?>
            {
                Success = false,
                Message = "Livro ou usuario não encontrado."
            };
        }  

        if (book.IsLoaned)
        {
            return new ServiceResult<LoanResponseDto?>
            {
                Success = false,
                Message = "O livro já está alugado."
            };
        }

        Loan loan = new()
        {
            BookId = book.Id,
            UserId = user.Id,
            LoanDate = DateTime.Now,
        };

        _context.Loans.Add(loan);

        book.IsLoaned = true;

        _context.SaveChanges();

        LoanResponseDto loanResponseDto = new()
        {
            Id = loan.Id,
            UserName = user.Name,
            BookTitle = book.Title,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            Status = "Ativo"
        };

        return new ServiceResult<LoanResponseDto?>
        {
            Success = true,
            Data = loanResponseDto
        };
    }

    public ServiceResult<LoanResponseDto?> PostReturn(int id)
    {
        var loan = _context.Loans.FirstOrDefault(x => x.Id == id);

        if (loan == null)
        {
            return new ServiceResult<LoanResponseDto?>()
            {
                Success = false,
                Message = "Empréstimo não encontrado."
            };
        }
        
        if (loan.ReturnDate != null)
        {
            return new ServiceResult<LoanResponseDto?>
            {
                Success = false,
                Message = "Livro já devolvido."
            };
        }

        var book = _context.Books.FirstOrDefault(x => x.Id == loan.BookId);
        var user = _context.Users.FirstOrDefault(x => x.Id == loan.UserId);

        loan.ReturnDate = DateTime.Now;
        book.IsLoaned = false;

        _context.SaveChanges();

        LoanResponseDto loanResponseDto = new()
        {
            Id = loan.Id,
            BookTitle = book.Title,
            UserName = user.Name,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            Status = "Devolvido"
        };

        return new ServiceResult<LoanResponseDto?>()
        {
            Success = true,
            Data = loanResponseDto
        };
    }
}
