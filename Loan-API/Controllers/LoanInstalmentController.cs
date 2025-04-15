using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanInstalmentController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public LoanInstalmentController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("instalment/{id}")]
        public async Task<IActionResult> GetInstalmentById(int id)
        {
            try
            {
                var result = await _unitOfWork.LoanInstalment.GetInstalmentDetailsByIdAsync(id);

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "Instalment not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }
        [HttpGet]
        [Route("instalments-by-month")]
        public async Task<IActionResult> GetInstalmentsByMonth([FromQuery] DateTime date)
        {
            try
            {
                var result = await _unitOfWork.LoanInstalment.GetInstalmentsByMonthAsync(date);

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "No instalments found for this month!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpPut("paid/{id}")]
        public async Task<IActionResult> MarkLoanInstalmentAsPaid(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync(); // Begin DB transaction
            try
            {
                var instalment = await _unitOfWork.LoanInstalment.GetByIdAsync(id);
                if (instalment == null)
                {
                    return NotFound(new { StatusCode = 404, Message = $"Loan instalment with ID {id} not found." });
                }

                if (instalment.Status == 1) // Already paid
                {
                    return BadRequest(new { StatusCode = 400, Message = "Loan instalment already marked as paid." });
                }

                // Mark as paid
                instalment.Status = 1;
                await _unitOfWork.LoanInstalment.UpdateAsync(instalment);

                // Deduct from loan due amount
                var loan = await _unitOfWork.Loan.GetByIdAsync(instalment.LoanID);
                if (loan == null)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Associated loan not found." });
                }

                loan.DueAmount -= instalment.AmountPaid;
                if (loan.DueAmount < 0) loan.DueAmount = 0;

                loan.PaidAmount += instalment.AmountPaid;

                await _unitOfWork.Loan.UpdateAsync(loan);

                // Get Customer ID from Loan
                var customerId = loan.CustomerID;

                //Insert into Transaction table
                var transactionRecord = new Transaction
                {
                    TransactionType = 2,
                    Amount = instalment.AmountPaid,
                    TransactionDate = DateTime.UtcNow,
                    CustomerId = customerId,
                    //LoanID = loan.LoanID,
                    PaytMethodID = instalment.PayMethodId ?? 0,
                    Remarks = $"Installment ID {instalment.InstalmentID} paid"
                };

                await _unitOfWork.Transction.AddAsync(transactionRecord);

                // Save everything
                await _unitOfWork.Save();
                await transaction.CommitAsync();

                return Ok(new { StatusCode = 200, Message = "Loan instalment marked as paid. Loan updated and transaction recorded." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { StatusCode = 500, Message = "An error occurred", Error = ex.Message });
            }
        }



    }
}
