using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public WithdrawController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("withdraw-requests")]
        public async Task<IActionResult> GetAllWithdraws()
        {
            try
            {
                var result = await _unitOfWork.Withdraw.GetAllWithdrawDetailsAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "Withdraw requests not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }
        [HttpGet]
        [Route("withdraw-requests-by-customerId")]
        public async Task<IActionResult> GetAllWithdrawsByCustomerId(int customerId)
        {
            try
            {
                var result = await _unitOfWork.Withdraw.GetWithdrawDetailsByCustomerIdAsync(customerId);

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "Withdraw requests not found for the customer!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostWithdrawRequest([FromBody] WithdrawRequestDTO withdrawDto)
        {
            try
            {
                if (withdrawDto == null)
                {
                    return BadRequest("Withdraw request data cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // 👉 Step 1: Get customer's current balance

                var account = _unitOfWork.Account.GetAccountInfoCustomerId(withdrawDto.CustommerID);

                if (account == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Customer Account not found." });
                }

                // 👉 Step 2: Check if balance is enough
                if (account.BalanceAmount < withdrawDto.Amount)
                {
                    return BadRequest(new { StatusCode = 400, message = "Insufficient balance for withdrawal." });
                }

                // 👉 Step 3: If balance is enough, create the withdrawal request
                var withdrawRequest = new Withdraw
                {
                    PaymentMethodID = withdrawDto.PaymentMethodID,
                    BankName = withdrawDto.BankName,
                    AccountNumber = withdrawDto.AccountNumber,
                    Amount = withdrawDto.Amount,
                    RequestedDate = DateTime.Now,
                    CustommerID = withdrawDto.CustommerID
                };

                await _unitOfWork.Withdraw.AddAsync(withdrawRequest);
                await _unitOfWork.Save();

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Withdraw request submitted successfully.",
                    WithdrawRequestId = withdrawRequest.WithdrawaID
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveWithdrawRequest(int id,int userId, bool isApproved)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var withdraw = await _unitOfWork.Withdraw.GetByIdAsync(id);
                if (withdraw == null)
                {
                    return NotFound(new { StatusCode = 404, Message = $"Withdraw request with ID {id} not found." });
                }

                if (withdraw.IsApproved==true)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Withdraw request already approved." });
                }

                var customerId = withdraw.CustommerID;

                withdraw.IsApproved = isApproved;
                withdraw.ApproveAt = DateTime.UtcNow;
                withdraw.ApproveBy = userId;
                withdraw.AdminRemarks = "Approved"; // You can make this dynamic if needed
                await _unitOfWork.Withdraw.UpdateAsync(withdraw);

                var account = _unitOfWork.Account.GetAccountInfoCustomerId(customerId);

                if (account != null)
                {
                    if (account.BalanceAmount < withdraw.Amount)
                    {
                        return BadRequest(new { StatusCode = 400, Message = "Insufficient balance in customer's account." });
                    }

                    account.BalanceAmount -= withdraw.Amount;
                    await _unitOfWork.Account.UpdateAsync(account);
                }
                else
                {
                    return BadRequest(new { StatusCode = 400, Message = "Customer not found." });
                }

                var transactionRecord = new Transaction
                {
                    TransactionType = 4, // Assuming 4 = Withdraw
                    Amount = withdraw.Amount,
                    TransactionDate = DateTime.UtcNow,
                    CustomerId = customerId,
                    PaytMethodID = withdraw.PaymentMethodID,
                    Remarks = $"Withdraw approved for request ID {withdraw.WithdrawaID}"
                };

                await _unitOfWork.Transction.AddAsync(transactionRecord);

                await _unitOfWork.Save();
                await transaction.CommitAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Withdraw approved, account updated, and transaction recorded."
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while approving the withdraw.",
                    Error = ex.Message
                });
            }
        }


    }
}
