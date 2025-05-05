using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanPlanController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public LoanPlanController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("plan/{id}")]
        public async Task<IActionResult> GetLoanPlanById(int id)
        {
            try
            {
                var plan = await _unitOfWork.LoanPlan.GetByIdAsync(id);

                if (plan == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan plan not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = plan });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("plans")]
        public async Task<IActionResult> GetAllLoanPlan()
        {
            try
            {
                var result = await _unitOfWork.LoanPlan.GetAllActiveAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "No loan plans found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLoanPlan([FromBody] LoanPlanCreateDTO loanPlanDto)
        {
            try
            {
                if (loanPlanDto == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid data!" });
                }

                var loanPlan = new LoanPlan
                {
                    PlanName = loanPlanDto.PlanName,
                    MinAmount = loanPlanDto.MinAmount,
                    MaxAmount = loanPlanDto.MaxAmount,
                    InterestRate = loanPlanDto.InterestRate,
                    MinRepaymentPeriod = loanPlanDto.MinRepaymentPeriod,
                    MaxRepaymentPeriod = loanPlanDto.MaxRepaymentPeriod,
                    ProcessingFee = loanPlanDto.ProcessingFee,
                    LatePaymentPenalty = loanPlanDto.LatePaymentPenalty,
                    Descraption = loanPlanDto.Descraption,
                    IsActive = loanPlanDto.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    // Optionally set CreatedBy here
                };

                await _unitOfWork.LoanPlan.AddAsync(loanPlan);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Loan plan created successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLoanPlan(int id, [FromBody] LoanPlanCreateDTO loanPlanDto)
        {
            try
            {
                var existingPlan = await _unitOfWork.LoanPlan.GetByIdAsync(id);

                if (existingPlan == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan plan not found!" });
                }

                // Update all fields from DTO
                existingPlan.PlanName = loanPlanDto.PlanName;
                existingPlan.MinAmount = loanPlanDto.MinAmount;
                existingPlan.MaxAmount = loanPlanDto.MaxAmount;
                existingPlan.InterestRate = loanPlanDto.InterestRate;
                existingPlan.MinRepaymentPeriod = loanPlanDto.MinRepaymentPeriod;
                existingPlan.MaxRepaymentPeriod = loanPlanDto.MaxRepaymentPeriod;
                existingPlan.ProcessingFee = loanPlanDto.ProcessingFee;
                existingPlan.LatePaymentPenalty = loanPlanDto.LatePaymentPenalty;
                existingPlan.Descraption = loanPlanDto.Descraption;
                existingPlan.IsActive = loanPlanDto.IsActive;
                existingPlan.UpdatedAt = DateTime.UtcNow;
                // Optionally set UpdatedBy if you have user info

                _unitOfWork.LoanPlan.UpdateAsync(existingPlan);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Loan plan updated successfully", data = existingPlan });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLoanPlan(int id, [FromQuery] int userId)
        {
            try
            {
                var result =  _unitOfWork.LoanPlan.SoftDeleteAsync(id, userId);


                await _unitOfWork.Save();

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Loan plan deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    message = "An error occurred while deleting the loan plan.",
                    error = ex.Message
                });
            }
        }



    }
}
