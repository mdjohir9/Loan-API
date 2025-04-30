using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {

        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public LoanController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("loans")]
        public async Task<IActionResult> GetAllLoans()
        {
            try
            {
                // Retrieve all loan applications from the unit of work
                var result = await _unitOfWork.Loan.GetAllLoanDetailsAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "No loan applications found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("loan/{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            try
            {
                var result = await _unitOfWork.Loan.GetLoanDetailsByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("balance/{customerId}")]
        public async Task<IActionResult> GetLoanBalanceByCustomerId(int customerId)
        {
            try
            {
                var result = await _unitOfWork.Loan.GetLoanBalanceByCustomerIdAsync(customerId);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan balance not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


    }
}
