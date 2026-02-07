using Loan_API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransctionController : ControllerBase {

        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public TransctionController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("transactions")]
        public async Task<IActionResult> GetTransactionsByCustomerAndDateRange(int customerId, DateTime fromDate, DateTime toDate)
        {
          
            try
            {
                var result = await _unitOfWork.Transction.GetTransactionsByCustomerAndDateRangeAsync(customerId, fromDate, toDate);

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "No transactions found!" });
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
