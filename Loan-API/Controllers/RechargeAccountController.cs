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
        [Route("PaymentType")]
        public async Task<IActionResult> GetPaymentTypeById()
        {
            try
            {
                var result = await _unitOfWork.RechargePaymentMethod.GetAllAsync();

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



        [HttpGet]
        [Route("rechargeAccountByPaymentType/{id}")]
        public IActionResult GetRechargeAccountByPeymentTypes(int id)
        {
            try
            {
                var result = _unitOfWork.RechargeAccount.GetRechargeAccountsByPaymentType(id); // method name should be plural now

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "No recharge accounts found for this payment method!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpGet]
        [Route("rechargeAccounts")]
        public async Task<IActionResult> GetRechargeAccounts()
        {
            try
            {
                var result = await _unitOfWork.RechargeAccount.GetRechargeAccountsAsync();

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


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRechargeAccount([FromBody] RechargeAccountDTO newAccount)
        {
            try
            {
                if (newAccount == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid account data" });
                }

                // Manually map DTO to entity
                var rechargeAccount = new RechargeAccount
                {
                    RecPaymentMethodId = newAccount.RecPaymentMethodId,
                    BankOrWalletName = newAccount.BankOrWalletName,
                    AccountName = newAccount.AccountName,
                    AccountNumber = newAccount.AccountNumber,
                    IsActive = newAccount.IsActive
                };

                await _unitOfWork.RechargeAccount.AddAsync(rechargeAccount);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Recharge Account created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateRechargeAccount(int id, [FromBody] RechargeAccountDTO updatedAccount)
        {
            try
            {
                var existingAccount = await _unitOfWork.RechargeAccount.GetByIdAsync(id);
                if (existingAccount == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Recharge Account not found!" });
                }

                // Update fields
                existingAccount.RecPaymentMethodId = updatedAccount.RecPaymentMethodId;
                existingAccount.BankOrWalletName = updatedAccount.BankOrWalletName;
                existingAccount.AccountName = updatedAccount.AccountName;
                existingAccount.AccountNumber = updatedAccount.AccountNumber;
                existingAccount.IsActive = updatedAccount.IsActive;

                await _unitOfWork.RechargeAccount.UpdateAsync(existingAccount);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Recharge Account updated successfully", data = existingAccount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteRechargeAccount(int id)
        {
            try
            {
                var existingAccount = await _unitOfWork.RechargeAccount.GetByIdAsync(id);
                if (existingAccount == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Recharge Account not found!" });
                }

                await _unitOfWork.RechargeAccount.DeleteAsync(existingAccount.Id);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Recharge Account deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


    }
}
