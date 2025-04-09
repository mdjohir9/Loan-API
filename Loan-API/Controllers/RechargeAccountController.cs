using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RechargeAccountController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public RechargeAccountController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("rechargeAccount/{id}")]
        public async Task<IActionResult> GetRechargeAccountById(int id)
        {
            try
            {
                var result = await _unitOfWork.RechargeAccount.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Recharge Account not found!" });
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
