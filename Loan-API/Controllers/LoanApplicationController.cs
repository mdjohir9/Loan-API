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
    public class LoanApplicationController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public LoanApplicationController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("application/{id}")]
        public async Task<IActionResult> GetLoanApplicationById(int id)
        {
            try
            {
                // Retrieve the loan application by ID from the unit of work
                var result = await _unitOfWork.LoanApplication.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan application not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }
        [HttpGet("applications")]
        public async Task<IActionResult> GetAllLoanApplications()
        {
            try
            {
                // Retrieve all loan applications from the unit of work
                var result = await _unitOfWork.LoanApplication.GetAllLoanApplicationsWithDetailsAsync();

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

        [HttpGet("calculate-emi")]
        public async Task<IActionResult> CalculateEMI(decimal LoanAmount,int RepaymentPeriod, int PlanID)
        {
            if (LoanAmount <= 0 || RepaymentPeriod <= 0)
            {
                return BadRequest(new { StatusCode = 400, message = "Invalid input values" });
            }

            var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(PlanID);
            if (loanPlan == null)
            {
                return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });
            }

            // Get the interest rate and calculate the monthly rate
            decimal interestRate = loanPlan.InterestRate;
            var result = await _unitOfWork.LoanApplication.CalculateEMIAsync(
                LoanAmount, interestRate, RepaymentPeriod
            );

            return Ok(new { StatusCode = 200, message = "Success", data = result });
        }

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveLoanApplication(int id, [FromBody] LoanApplicationDTO approveDto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync(); // Begin Transaction

            try
            {
                // Retrieve the loan application by ID
                var loanApplication = await _unitOfWork.LoanApplication.GetByIdAsync(id);
                if (loanApplication == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan application not found!" });
                }

                // Check if already approved
                if (loanApplication.Status == 1) // 1 = Approved
                {
                    return BadRequest(new { StatusCode = 400, message = "Loan application is already approved." });
                }

                var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(approveDto.PlanID);
                if (loanPlan == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });
                }
                decimal interestRate = loanPlan.InterestRate;

                var result = await _unitOfWork.LoanApplication.CalculateEMIAsync(
           approveDto.LoanAmount, interestRate, approveDto.RepaymentPeriod);

                int repaymentPeriod = approveDto.RepaymentPeriod;

                loanApplication.Status = 1; 
                loanApplication.PlanID = approveDto.PlanID;
                loanApplication.LoanAmount = approveDto.LoanAmount;
                loanApplication.RepaymentPeriod = approveDto.RepaymentPeriod;
                loanApplication.PurposeOfLoan = approveDto.PurposeOfLoan;
                loanApplication.HasExistingLoans = false;
                loanApplication.ExistingLoanAmount = 0;
                loanApplication.LenderName = "Upstartloan";
                loanApplication.MonthlyInstallments = result.MonthlyInstallment;
                loanApplication.ApplicationDate = approveDto.ApplicationDate;
                loanApplication.PayMethodID = approveDto.PayMethodID;
                loanApplication.ApprovedBy = approveDto.ApprovedBy;
                loanApplication.ApprovedAt = DateTime.UtcNow;

                _unitOfWork.LoanApplication.UpdateAsync(loanApplication);
                await _unitOfWork.Save();

                // Create Loan Entry
                var newLoan = new Loan
                {
                    LoanNumber = Guid.NewGuid().ToString().Substring(0, 10).ToUpper(), // Generate unique Loan Number
                    LoanAmount = approveDto.LoanAmount,
                    PaidAmount = 0,
                    TotalPayableAmount= result.TotalPayable,
                    TotalInterest = result.TotalInterest,
                    MonthlyInstallment = result.MonthlyInstallment,
                    DueAmount = result.TotalPayable,
                    TenureMonths = approveDto.RepaymentPeriod,
                    LoanStartDate = DateTime.UtcNow,
                    LoanEndDate = DateTime.UtcNow.AddMonths(approveDto.RepaymentPeriod),
                    LoanStatus = 1, // 1 = Active
                    Purpose = approveDto.PurposeOfLoan,
                    CustomerID = approveDto.CustomerID,
                    PayMethodId = approveDto.PayMethodID,
                    DisbursementDate = DateTime.UtcNow,
                    LateCharge =result.LateCharge,
                    DepositAmount = result.DepositAmount,

                };

                await _unitOfWork.Loan.AddAsync(newLoan);
                await _unitOfWork.Save();


        

                List<LoanInstalment> instalments = new List<LoanInstalment>();
                DateTime loanStartDate = DateTime.UtcNow; 

                for (int i = 1; i <= repaymentPeriod; i++)
                {
                    var instalment = new LoanInstalment
                    {
                        LoanID = newLoan.LoanID,
                        PaymentDate = DateOnly.FromDateTime(loanStartDate.AddMonths(i)), 
                        Status = 0, 
                        PayMethodId = approveDto.PayMethodID,
                        AmountPaid = result.MonthlyInstallment 
                    };
                    instalments.Add(instalment);
                }

                // Save Installments
                await _unitOfWork.LoanInstalment.AddRangeAsync(instalments);
                await _unitOfWork.Save();


                // Commit Transaction
                await transaction.CommitAsync();

                return Ok(new { StatusCode = 200, message = "Loan application approved, Loan created, and Installments scheduled." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback if an error occurs
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectLoanApplication(int id)
        {
            try
            {
                // Retrieve the loan application by ID
                var loanApplication = await _unitOfWork.LoanApplication.GetByIdAsync(id);
                if (loanApplication == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan application not found!" });
                }

                // Check if the loan is already rejected
                if (loanApplication.Status == 2) // Assuming 2 = Rejected
                {
                    return BadRequest(new { StatusCode = 400, message = "Loan application is already rejected." });
                }

                // Update the loan application status to Rejected (2)
                loanApplication.Status = 2; // 2 = Rejected
                _unitOfWork.LoanApplication.UpdateAsync(loanApplication);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Loan application rejected successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLoanApplication([FromBody] LoanApplicationDTO loanDto)
        {
            try
            {
                if (loanDto == null)
                    return BadRequest("Loan application details cannot be null.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var account =  _unitOfWork.Account.GetAccountInfoCustomerId(loanDto.CustomerID);
                if (account == null)
                    return BadRequest(new { StatusCode = 400, message = "Account not found for the given Customer ID." });

                var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(loanDto.PlanID);
                if (loanPlan == null)
                    return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });

                decimal interestRate = loanPlan.InterestRate;

                var result = await _unitOfWork.LoanApplication.CalculateEMIAsync(
                    loanDto.LoanAmount, interestRate, loanDto.RepaymentPeriod);

                if (account.BalanceAmount < result.DepositAmount)
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        message = $"Your wallet balance is too low to continue. Please deposit at least {result.DepositAmount:C} to meet the minimum requirement. " +
                                  "To apply for a loan, you need to have at least 5% of the requested loan amount in your wallet as a confirmation of your repayment ability."
                    });

                // Begin transaction
                using var transaction = await _unitOfWork.BeginTransactionAsync();

                try
                {
                    // Deduct deposit from account
                    account.BalanceAmount -= result.DepositAmount;
                    await _unitOfWork.Account.UpdateAsync(account);

                    var loanApplication = new LoanApplication
                    {
                        CustomerID = loanDto.CustomerID,
                        PlanID = loanDto.PlanID,
                        LoanAmount = loanDto.LoanAmount,
                        RepaymentPeriod = loanDto.RepaymentPeriod,
                        PurposeOfLoan = loanDto.PurposeOfLoan,
                        HasExistingLoans = false,
                        ExistingLoanAmount = 0,
                        LenderName = "Upstartloan",
                        MonthlyInstallments = result.MonthlyInstallment,
                        Status = 0,
                        ApplicationDate = loanDto.ApplicationDate,
                        PayMethodID = loanDto.PayMethodID,
                        LateCharge = result.LateCharge,
                        DepositAmount = result.DepositAmount,
                    };

                    await _unitOfWork.LoanApplication.AddAsync(loanApplication);

                    await _unitOfWork.Save(); // Save both operations

                    await transaction.CommitAsync(); // Commit transaction

                    return Ok(new { StatusCode = 200, message = "Loan application created successfully." });
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(); // Rollback on error
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLoanApplication(int id, [FromBody] LoanApplicationDTO loanDto)
        {
            try
            {
                if (loanDto == null)
                {
                    return BadRequest("Loan application details cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingLoan = await _unitOfWork.LoanApplication.GetByIdAsync(id);
                if (existingLoan == null)
                {
                    return NotFound($"Loan application with ID {id} not found.");
                }
                var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(loanDto.PlanID);
                if (loanPlan == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });
                }
                decimal interestRate = loanPlan.InterestRate;

                var result = await _unitOfWork.LoanApplication.CalculateEMIAsync(
           loanDto.LoanAmount, interestRate, loanDto.RepaymentPeriod);

                // Update properties
                existingLoan.CustomerID = loanDto.CustomerID;
                existingLoan.PlanID = loanDto.PlanID;
                existingLoan.LoanAmount = loanDto.LoanAmount;
                existingLoan.RepaymentPeriod = loanDto.RepaymentPeriod;
                existingLoan.PurposeOfLoan = loanDto.PurposeOfLoan;
                existingLoan.HasExistingLoans = false;
                existingLoan.ExistingLoanAmount = 0;
                existingLoan.LenderName =  "Upstartloan"; 
                existingLoan.MonthlyInstallments = result.MonthlyInstallment;
                existingLoan.Status = 0;
                existingLoan.ApplicationDate = loanDto.ApplicationDate;
                existingLoan.PayMethodID = loanDto.PayMethodID;

                // Save changes
                _unitOfWork.LoanApplication.UpdateAsync(existingLoan);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Loan application updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        //// Generate Loan Instalments
        //var instalments = new List<LoanInstalment>();
        //decimal monthlyInstalmentAmount = approveDto.LoanAmount / approveDto.RepaymentPeriod;
        //DateTime loanStartDate = DateTime.UtcNow; // Loan start date

        //for (int i = 1; i <= approveDto.RepaymentPeriod; i++)
        //{
        //    var instalment = new LoanInstalment
        //    {
        //        LoanID = newLoan.LoanID,
        //        PaymentDate = DateOnly.FromDateTime(loanStartDate.AddMonths(i)), // Set payment date relative to loan start date
        //        Status = 0, // 0 = Pending
        //        PayMethodId = approveDto.PayMethodID,
        //        AmountPaid = monthlyInstalmentAmount
        //    };
        //    instalments.Add(instalment);
        //}
        // Retrieve the Loan Plan by PlanID
    }
}
