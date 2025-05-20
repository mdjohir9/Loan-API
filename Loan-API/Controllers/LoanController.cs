using Loan_API.DTO;
using Loan_API.Entities;
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

        [HttpPost("signature/create")]
        public async Task<IActionResult> PostFullCustomer([FromBody] CustomerSignatureDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest("Customer information cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using var transaction = await _unitOfWork.BeginTransactionAsync(); // Begin transaction

            try
            {
                string companyId = "1111";
                string? imageResult = null;
                string? signatureResult = null;

                var result = await _unitOfWork.Custommer.GetByIdAsync(customerDto.CustomerId);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Customer not found!" });
                }

                if (customerDto.CustommerSignature != null && customerDto.CustommerSignature.Any())
                {
                    string DocumentTypeSigImage = "CustommerSignature";
                    signatureResult = await _unitOfWork.Custommer.SaveDocumentsListsAsync(
                        customerDto.CustommerSignature,
                        result.CustCardNo,
                        companyId,
                        DocumentTypeSigImage
                    );
                }


                result.CustommerSignature = signatureResult;
                _unitOfWork.Custommer.UpdateAsync(result);



                await _unitOfWork.Save();

                await transaction.CommitAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Customer with full details created successfully."
                    
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }



    }
}
