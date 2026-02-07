using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Implementation;
using Loan_API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustommerFinancialController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public CustommerFinancialController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("financial/{id}")]
        public async Task<IActionResult> GetFinancialInfoById(int id)
        {
            try
            {
                // Retrieve the financial info by ID from the unit of work
                var result = await _unitOfWork.FinancialInfo.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Customer financial information not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostCustomerFinancialInfo([FromBody] CustommerFinancialInfoDTO financialInfoDto)
        {
            try
            {
                if (financialInfoDto == null)
                {
                    return BadRequest("Customer financial information cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if financial information already exists
                var existingFinancialInfo =  _unitOfWork.FinancialInfo.GetCustommerFinancialByCustomerId(financialInfoDto.CustomerID);

                if (existingFinancialInfo != null)
                {
                    // If financial information exists, update it
                    return await UpdateCustomerFinancialInfo(financialInfoDto, financialInfoDto.CustomerID);
                }

                // If no existing record, create a new one
                var financialInfo = new CustommerFinancialInfo
                {
                    CustomerID = financialInfoDto.CustomerID,
                    BankName = financialInfoDto.BankName,
                    AccountNumber = financialInfoDto.AccountNumber,
                    MonthlyIncomeSources = financialInfoDto.MonthlyIncomeSources,
                    MonthlyExpenses = financialInfoDto.MonthlyExpenses,
                    AssetsOwned = financialInfoDto.AssetsOwned,
                    Liabilities = financialInfoDto.Liabilities
                };


                var account = new AccountBalance
                {
                    CustomerId = financialInfoDto.CustomerID,
                    AccountNo = await _unitOfWork.Account.GenerateUniqueAccountNumberAsync(),
                    BalanceAmount = 0,
                    IsActive = 1,
                    CreatedBy = financialInfoDto.UserId,
                    CreatedAt = DateTime.Now,



                };

                await _unitOfWork.FinancialInfo.AddAsync(financialInfo);

                await _unitOfWork.Account.AddAsync(account);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Customer financial information saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        private string GenerateAccountNumber()
        {
            // You can customize this format
            Random random = new Random();
            return random.Next(10000000, 99999999).ToString() + random.Next(1000, 9999).ToString();
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCustomerFinancialInfo([FromBody] CustommerFinancialInfoDTO financialInfoDto, int id)
        {
            try
            {
                if (financialInfoDto == null)
                {
                    return BadRequest("Customer financial information cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Use UpdateByForeignKeyAsync to update financial details based on CustomerID
                await _unitOfWork.FinancialInfo.UpdateByForeignKeyAsync<CustommerFinancialInfo>(
                    f => f.CustomerID == id,
                    existingFinancialInfo =>
                    {
                        existingFinancialInfo.BankName = financialInfoDto.BankName;
                        existingFinancialInfo.AccountNumber = financialInfoDto.AccountNumber;
                        existingFinancialInfo.MonthlyIncomeSources = financialInfoDto.MonthlyIncomeSources;
                        existingFinancialInfo.MonthlyExpenses = financialInfoDto.MonthlyExpenses;
                        existingFinancialInfo.AssetsOwned = financialInfoDto.AssetsOwned;
                        existingFinancialInfo.Liabilities = financialInfoDto.Liabilities;
                    }
                );

                 await _unitOfWork.Save();
                return Ok(new { StatusCode = 200, message = "Customer financial information updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



    }
}
