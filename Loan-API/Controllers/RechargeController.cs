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
    public class RechargeController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public RechargeController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveRechargeRequest(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var recharge = await _unitOfWork.Recharge.GetByIdAsync(id);
                if (recharge == null)
                {
                    return NotFound(new { StatusCode = 404, Message = $"Recharge request with ID {id} not found." });
                }

                if (recharge.IsApproved)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Recharge request already approved." });
                }

                var customerId = recharge.CustommerID;

                // Update recharge approval
                recharge.IsApproved = true;
                recharge.AdminRemarks = "Approved"; // optional
                await _unitOfWork.Recharge.UpdateAsync(recharge);

                // Update or insert into AccountBalance
                var account = _unitOfWork.Account.GetAccountInfoCustomerId(recharge.CustommerID);

                if (account != null)
                {
                    account.BalanceAmount += recharge.Amount;
                    _unitOfWork.Account.UpdateAsync(account);
                }
                else
                {
                    return BadRequest(new { StatusCode = 400, Message = "Custommer Not Found" });

                }

                // Insert into Transaction table
                var transactionRecord = new Transaction
                {
                    TransactionType = 3,
                    Amount = recharge.Amount,
                    TransactionDate = DateTime.UtcNow,
                    CustomerId = customerId,
                    PaytMethodID = recharge.PaymentMethodID,
                   // BankID = recharge.BankId,
                    Remarks = $"Recharge approved for request ID {recharge.RechargeID}"
                };

                await _unitOfWork.Transction.AddAsync(transactionRecord);

                // Save changes
                await _unitOfWork.Save();
                await transaction.CommitAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Recharge approved, account updated, and transaction recorded."
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while approving the recharge.",
                    Error = ex.Message
                });
            }
        }


        [HttpGet]
        [Route("recharge-requerts")]
        public async Task<IActionResult> GetAllRechagres()
        {
            try
            {
                var result = await _unitOfWork.Recharge.GetAllRechargeDetailsAsync();

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

        [HttpPost("create")]
        public async Task<IActionResult> PostRechargeRequest([FromBody] RechargeRequestDTO rechargeDto)
        {
            try
            {
                if (rechargeDto == null)
                {
                    return BadRequest("Recharge request data cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string documentResult = null;
                string CompanyId = "0001"; // static company ID, change if needed

                // Process Statement Document if available
                if (rechargeDto.Statement != null && rechargeDto.Statement.Any())
                {
                    string documentType = "RechargeStatement";
                    documentResult = await _unitOfWork.Custommer.SaveDocumentsListsAsync(
                         rechargeDto.Statement ,
                        rechargeDto.BankTransactCode.ToString(), // using Customer ID as reference
                        CompanyId,
                        documentType
                    );
                }

                var rechargeRequest = new Recharge
                {
                    BankAccountNumber = rechargeDto.BankAccountNumber,
                    Amount = rechargeDto.Amount,
                    RequestedDate = rechargeDto.RequestedDate,
                    IsApproved = rechargeDto.IsApproved,
                    BankTransactCode = rechargeDto.BankTransactCode,
                    AdminRemarks = rechargeDto.AdminRemarks,
                    Statement = documentResult, // Save the path or reference to the uploaded document
                    PaymentMethodID = rechargeDto.PaymentMethodID,
                    BankId = rechargeDto.BankId,
                    CustommerID = rechargeDto.CustommerID
                };

                // Save recharge request to the database
                await _unitOfWork.Recharge.AddAsync(rechargeRequest);
                await _unitOfWork.Save();

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Recharge request submitted successfully.",
                    RechargeRequestId = rechargeRequest.RechargeID // assuming there's an identity key
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
