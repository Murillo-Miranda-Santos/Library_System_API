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
        public async Task<IActionResult> Get()
        {
            var loanResponseDtos = await _loanService.GetAllLoans();

            return Ok(loanResponseDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var loanResponseDto = await _loanService.GetLoan(id);

            if (loanResponseDto == null)
                return NotFound("Loan not found.");

            return Ok(loanResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateLoanDto createLoanDto)
        {
            var result = await _loanService.PostLoan(createLoanDto);

            if (result.Success == false)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("return/{id}")]
        public async Task<IActionResult> Return(int id)
        {
            var result = await _loanService.PostReturn(id);

            if (result.Success == false)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}