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

        [HttpGet("loan/{id}")]
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
        [HttpGet("loans")]
        public async Task<IActionResult> GetAllLoanApplications()
        {
            try
            {
                // Retrieve all loan applications from the unit of work
                var result = await _unitOfWork.LoanApplication.GetAllAsync();

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

                // Update the loan application status to Approved
                loanApplication.Status = 1; // 1 = Approved
                loanApplication.PlanID = approveDto.PlanID;
                loanApplication.LoanAmount = approveDto.LoanAmount;
                loanApplication.RepaymentPeriod = approveDto.RepaymentPeriod;
                loanApplication.PurposeOfLoan = approveDto.PurposeOfLoan;
                loanApplication.HasExistingLoans = false;
                loanApplication.ExistingLoanAmount = 0;
                loanApplication.LenderName = "Upstartloan";
                loanApplication.MonthlyInstallments = approveDto.MonthlyInstallments;
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
                    DueAmount = approveDto.LoanAmount,
                    TenureMonths = approveDto.RepaymentPeriod,
                    LoanStartDate = DateTime.UtcNow,
                    LoanEndDate = DateTime.UtcNow.AddMonths(approveDto.RepaymentPeriod),
                    LoanStatus = 1, // 1 = Active
                    Purpose = approveDto.PurposeOfLoan,
                    CustomerID = approveDto.CustomerID,
                    PayMethodId = approveDto.PayMethodID,
                    DisbursementDate = DateTime.UtcNow
                };

                await _unitOfWork.Loan.AddAsync(newLoan);
                await _unitOfWork.Save();


                var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(approveDto.PlanID);
                if (loanPlan == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });
                }

                // Get the interest rate and calculate the monthly rate
                decimal interestRate = loanPlan.InterestRate; // Annual interest rate (percentage)
                decimal monthlyInterestRate = interestRate / 100 / 12; // Convert annual rate to monthly

                // Loan details
                decimal loanAmount = approveDto.LoanAmount;
                int repaymentPeriod = approveDto.RepaymentPeriod;

                // Calculate EMI using formula
                decimal monthlyInstalmentAmount;
                if (monthlyInterestRate > 0)
                {
                    monthlyInstalmentAmount = (loanAmount * monthlyInterestRate *
                        (decimal)Math.Pow((double)(1 + monthlyInterestRate), repaymentPeriod)) /
                        ((decimal)Math.Pow((double)(1 + monthlyInterestRate), repaymentPeriod) - 1);
                }
                else
                {
                    // If interest rate is 0 (for interest-free loans)
                    monthlyInstalmentAmount = loanAmount / repaymentPeriod;
                }

                // Generate Installments
                List<LoanInstalment> instalments = new List<LoanInstalment>();
                DateTime loanStartDate = DateTime.UtcNow; // Loan Start Date

                for (int i = 1; i <= repaymentPeriod; i++)
                {
                    var instalment = new LoanInstalment
                    {
                        LoanID = newLoan.LoanID,
                        PaymentDate = DateOnly.FromDateTime(loanStartDate.AddMonths(i)), // Monthly installment date
                        Status = 0, // 0 = Pending
                        PayMethodId = approveDto.PayMethodID,
                        AmountPaid = monthlyInstalmentAmount
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
                {
                    return BadRequest("Loan application details cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
                    MonthlyInstallments = loanDto.MonthlyInstallments,
                    Status = loanDto.Status,
                    ApplicationDate = loanDto.ApplicationDate,
                    PayMethodID = loanDto.PayMethodID
                };


                await _unitOfWork.LoanApplication.AddAsync(loanApplication);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Loan application created successfully." });
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

                // Update properties
                existingLoan.CustomerID = loanDto.CustomerID;
                existingLoan.PlanID = loanDto.PlanID;
                existingLoan.LoanAmount = loanDto.LoanAmount;
                existingLoan.RepaymentPeriod = loanDto.RepaymentPeriod;
                existingLoan.PurposeOfLoan = loanDto.PurposeOfLoan;
                existingLoan.HasExistingLoans = false;
                existingLoan.ExistingLoanAmount = 0;
                existingLoan.LenderName =  "Upstartloan"; 
                existingLoan.MonthlyInstallments = loanDto.MonthlyInstallments;
                existingLoan.Status = loanDto.Status;
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
