using LibrarySystemAPI.DTOs.loans;
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
            var loanResponseDtos = _loanService.GetAllLoans();

            return Ok(loanResponseDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var loanResponseDto = _loanService.GetLoan(id);

            if (loanResponseDto == null)
                return NotFound("Loan not found.");

            return Ok(loanResponseDto);
        }

        [HttpPost]
        public IActionResult Post(CreateLoanDto createLoanDto)
        {
            var result = _loanService.PostLoan(createLoanDto);

            if (result.Success == false)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("return/{id}")]
        public IActionResult Return(int id)
        {
            var result = _loanService.PostReturn(id);

            if (result.Success == false)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}