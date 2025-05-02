using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public DashboardController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("admin-balance")]
        public async Task<IActionResult> GetAdminDashboardBalance()
        {
            try
            {
                var result = await _unitOfWork.Transction.GetAdminDashboardSummaryAsync();

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("repayment-disbursed/{year}")]
        public async Task<IActionResult> GetAdminDashboardBalance(int year)
        {
            try
            {
                var result = await _unitOfWork.Transction.GetrepaymentAndDisbursedSummaryAsync(year);
                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

    }
}
