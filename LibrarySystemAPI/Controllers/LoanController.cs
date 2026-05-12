using LibrarySystemAPI.Models;
using LibrarySystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemAPI.Controllers
{
    [ApiController]
    [Route("loans")]
    public class LoanController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoanController(LoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_loanService.GetAllLoans());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var loan = _loanService.GetLoan(id);

            if (loan == null)
                return NotFound("Loan not found.");

            return Ok(loan);
        }

        [HttpPost]
        public IActionResult Post(Loan loan)
        {
            var result = _loanService.PostLoan(loan);

            if (result == null)
                return NotFound("User or book not found.");

            if (result == false)
                return BadRequest("Book already loaned.");

            return Ok(loan);
        }

        [HttpPost("return/{id}")]
        public IActionResult Return(int id)
        {
            var result = _loanService.PostReturn(id);

            if (result == null)
                return NotFound("Loan not found.");

            if (result == false)
                return BadRequest("Book already returned.");

            return Ok("Book returned successfully.");
        }
    }
}