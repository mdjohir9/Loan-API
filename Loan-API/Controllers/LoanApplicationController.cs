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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [HttpGet("applications/{customerId}")]
        public async Task<IActionResult> GetLoanApplicationsByCustomerId(int customerId)
        {
            try
            {
                var result = await _unitOfWork.LoanApplication.GetLoanApplicationsByCustomerIdAsync(customerId);

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "No loan applications found for the customer!" });
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
                LoanAmount, interestRate, RepaymentPeriod,loanPlan.ProcessingFee, loanPlan.LatePaymentPenalty
            );

            return Ok(new { StatusCode = 200, message = "Success", data = result });
        }

        [HttpGet("loan-limits/{planId}")]
        public async Task<IActionResult> GetLoanLimitsByPlanId(int planId)
        {
            try
            {
                var result = await _unitOfWork.LoanApplication.GetLoanLimitsByPlanIdAsync(planId);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan plan not found" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "Error occurred", error = ex.Message });
            }
        }





        //[HttpPut("approve/{id}")]
        //public async Task<IActionResult> ApproveLoanApplication(int id, [FromBody] LoanApplicationDTO approveDto)
        //{
        //    using var transaction = await _unitOfWork.BeginTransactionAsync(); // Begin Transaction

        //    try
        //    {
        //        // Retrieve the loan application by ID
        //        var loanApplication = await _unitOfWork.LoanApplication.GetByIdAsync(id);
        //        if (loanApplication == null)
        //        {
        //            return NotFound(new { StatusCode = 404, message = "Loan application not found!" });
        //        }

        //        // Check if already approved
        //        if (loanApplication.Status == 1) // 1 = Approved
        //        {
        //            return BadRequest(new { StatusCode = 400, message = "Loan application is already approved." });
        //        }

        //        var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(approveDto.PlanID);
        //        if (loanPlan == null)
        //        {
        //            return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });
        //        }
        //        decimal interestRate = loanPlan.InterestRate;

        //        var result = await _unitOfWork.LoanApplication.CalculateEMIAsync(
        //   approveDto.LoanAmount, interestRate, approveDto.RepaymentPeriod);

        //        int repaymentPeriod = approveDto.RepaymentPeriod;

        //        loanApplication.Status = 1; 
        //        loanApplication.PlanID = approveDto.PlanID;
        //        loanApplication.LoanAmount = approveDto.LoanAmount;
        //        loanApplication.RepaymentPeriod = approveDto.RepaymentPeriod;
        //        loanApplication.PurposeOfLoan = approveDto.PurposeOfLoan;
        //        loanApplication.HasExistingLoans = false;
        //        loanApplication.ExistingLoanAmount = 0;
        //        loanApplication.LenderName = "Upstartloan";
        //        loanApplication.MonthlyInstallments = result.MonthlyInstallment;
        //        loanApplication.ApplicationDate = approveDto.ApplicationDate;
        //        loanApplication.PayMethodID = approveDto.PayMethodID;
        //        loanApplication.ApprovedBy = approveDto.ApprovedBy;
        //        loanApplication.ApprovedAt = DateTime.UtcNow;

        //        _unitOfWork.LoanApplication.UpdateAsync(loanApplication);
        //        await _unitOfWork.Save();

        //        // Create Loan Entry
        //        var newLoan = new Loan
        //        {
        //            LoanNumber = Guid.NewGuid().ToString().Substring(0, 10).ToUpper(), // Generate unique Loan Number
        //            LoanAmount = approveDto.LoanAmount,
        //            PaidAmount = 0,
        //            TotalPayableAmount= result.TotalPayable,
        //            TotalInterest = result.TotalInterest,
        //            MonthlyInstallment = result.MonthlyInstallment,
        //            DueAmount = result.TotalPayable,
        //            TenureMonths = approveDto.RepaymentPeriod,
        //            LoanStartDate = DateTime.UtcNow,
        //            LoanEndDate = DateTime.UtcNow.AddMonths(approveDto.RepaymentPeriod),
        //            LoanStatus = 1, // 1 = Active
        //            Purpose = approveDto.PurposeOfLoan,
        //            CustomerID = approveDto.CustomerID,
        //            PayMethodId = approveDto.PayMethodID,
        //            DisbursementDate = DateTime.UtcNow,
        //            DepositAmount = result.DepositAmount,
        //            PlanID=approveDto.PlanID,

        //        };

        //        await _unitOfWork.Loan.AddAsync(newLoan);
        //        await _unitOfWork.Save();




        //        List<LoanInstalment> instalments = new List<LoanInstalment>();
        //        DateTime loanStartDate = DateTime.UtcNow;
        //        var account =  _unitOfWork.Account.GetAccountInfoCustomerId(approveDto.CustomerID);

        //        for (int i = 1; i <= repaymentPeriod; i++)
        //        {
        //            var instalment = new LoanInstalment
        //            {
        //                LoanID = newLoan.LoanID,
        //                PaymentDate = DateOnly.FromDateTime(loanStartDate.AddMonths(i)), 
        //                Status = 0, 
        //                PayMethodId = approveDto.PayMethodID,
        //                AmountPaid = result.MonthlyInstallment,
        //                LateCharge = result.LateCharge,
        //                AccountId= account.Id,

        //            };
        //            instalments.Add(instalment);
        //        }

        //        // Save Installments
        //        await _unitOfWork.LoanInstalment.AddRangeAsync(instalments);
        //        await _unitOfWork.Save();


        //        // Commit Transaction
        //        await transaction.CommitAsync();

        //        return Ok(new { StatusCode = 200, message = "Loan application approved, Loan created, and Installments scheduled." });
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync(); // Rollback if an error occurs
        //        return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
        //    }
        //}

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveLoanApplication(int id, [FromBody] LoanApplicationDTO approveDto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync(); // Begin Transaction

            try
            {
                // Retrieve the loan application
                var loanApplication = await _unitOfWork.LoanApplication.GetByIdAsync(id);
                if (loanApplication == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan application not found!" });
                }

                // Check if already approved
                if (loanApplication.Status == 1)
                {
                    return BadRequest(new { StatusCode = 400, message = "Loan application is already approved." });
                }

                // Validate Loan Plan
                var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(approveDto.PlanID);
                if (loanPlan == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });
                }

                decimal interestRate = loanPlan.InterestRate;

                // Calculate EMI
                var result = await _unitOfWork.LoanApplication.CalculateEMIAsync(
                    approveDto.LoanAmount, interestRate, approveDto.RepaymentPeriod, loanPlan.ProcessingFee,loanPlan.LatePaymentPenalty);

                int repaymentPeriod = approveDto.RepaymentPeriod;

                // Update Loan Application
                loanApplication.Status = 1;
                //loanApplication.PlanID = approveDto.PlanID;
                //loanApplication.LoanAmount = approveDto.LoanAmount;
                //loanApplication.RepaymentPeriod = approveDto.RepaymentPeriod;
                //loanApplication.PurposeOfLoan = approveDto.PurposeOfLoan;
                //loanApplication.HasExistingLoans = false;
                //loanApplication.ExistingLoanAmount = 0;
                //loanApplication.LenderName = "Upstartloan";
                //loanApplication.MonthlyInstallments = result.MonthlyInstallment;
                //loanApplication.ApplicationDate = approveDto.ApplicationDate;
                //loanApplication.PayMethodID = approveDto.PayMethodID;
                loanApplication.ApprovedBy = approveDto.UserId;
                loanApplication.ApprovedAt = DateTime.UtcNow;

                _unitOfWork.LoanApplication.UpdateAsync(loanApplication);
                await _unitOfWork.Save();

                var loanApplicationExist = await _unitOfWork.Loan.GetByIdAsync(id);
                if (loanApplicationExist != null)
                {
                    return BadRequest(new { StatusCode = 400, message = "The Loan is Allready Exist and Approve " });
                }


                // Create Loan
                var newLoan = new Loan
                {
                    LoanNumber = Guid.NewGuid().ToString().Substring(0, 10).ToUpper(),
                    LoanAmount = approveDto.LoanAmount,
                    PaidAmount = 0,
                    TotalPayableAmount = result.TotalPayable,
                    TotalInterest = result.TotalInterest,
                    MonthlyInstallment = result.MonthlyInstallment,
                    DueAmount = result.TotalPayable,
                    TenureMonths = approveDto.RepaymentPeriod,
                    LoanStartDate = DateTime.UtcNow,
                    LoanEndDate = DateTime.UtcNow.AddMonths(approveDto.RepaymentPeriod),
                    LoanStatus = 1, // Active
                    Purpose = approveDto.PurposeOfLoan,
                    CustomerID = loanApplication.CustomerID,
                    PayMethodId = approveDto.PayMethodID,
                    DisbursementDate = DateTime.UtcNow,
                    DepositAmount = result.DepositAmount,
                    PlanID = approveDto.PlanID,
                    ApplicationID=id,
                };

                await _unitOfWork.Loan.AddAsync(newLoan);
                await _unitOfWork.Save();

                // Fetch Customer Account
                var account =  _unitOfWork.Account.GetAccountInfoCustomerId(loanApplication.CustomerID);
                if (account == null)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new { StatusCode = 400, message = "Customer account not found." });
                }

                // ✅ Insert into Transaction table
                var transactionRecord = new Transaction
                {
                    TransactionType = 1, // Loan Disbursement
                    Amount = approveDto.LoanAmount,
                    TransactionDate = DateTime.UtcNow,
                    CustomerId = loanApplication.CustomerID,
                    PaytMethodID = approveDto.PayMethodID,
                    Remarks = $"Loan disbursed. LoanID: {newLoan.LoanID}"
                };
                await _unitOfWork.Transction.AddAsync(transactionRecord);

                // ✅ Update Account Balance
                account.BalanceAmount += approveDto.LoanAmount;
                await _unitOfWork.Account.UpdateAsync(account);

                await _unitOfWork.Save();

                // Create Installments
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
                        AmountPaid = result.MonthlyInstallment,
                        LateCharge = result.LateCharge,
                        AccountId = account.Id
                    };
                    instalments.Add(instalment);
                }

                await _unitOfWork.LoanInstalment.AddRangeAsync(instalments);
                await _unitOfWork.Save();

                await transaction.CommitAsync();

                return Ok(new { StatusCode = 200, message = "Loan application approved, Loan created, funds disbursed, and installments scheduled." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectLoanApplication(int id, int userId)
        {
            try
            {
                // Retrieve the loan application by ID
                var loanApplication = await _unitOfWork.LoanApplication.GetByIdAsync(id);
                if (loanApplication == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Loan application not found!" });
                }
                if (loanApplication.Status==1)
                {
                    return BadRequest(new { StatusCode = 400, message = "Loan application is already Approved. so its not rejectable" });
                }

                
                if (loanApplication.Status == 2) // Assuming 2 = Rejected
                {
                    return BadRequest(new { StatusCode = 400, message = "Loan application is already rejected." });
                }

                // Update the loan application status to Rejected (2)
                loanApplication.Status = 2; // 2 = Rejected
                loanApplication.RejectedBy = userId;
                loanApplication.RejectAt = DateTime.Now;
                _unitOfWork.LoanApplication.UpdateAsync(loanApplication);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Loan application rejected successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateLoanApplication([FromBody] LoanApplicationDTO loanDto)
        //{
        //    try
        //    {
        //        if (loanDto == null)
        //            return BadRequest("Loan application details cannot be null.");

        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var account =  _unitOfWork.Account.GetAccountInfoCustomerId(loanDto.CustomerID);
        //        if (account == null)
        //            return BadRequest(new { StatusCode = 400, message = "Account Not Create For this custommer." });

        //        var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(loanDto.PlanID);
        //        if (loanPlan == null)
        //            return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });

        //        decimal interestRate = loanPlan.InterestRate;

        //        var result = await _unitOfWork.LoanApplication.CalculateEMIAsync(
        //            loanDto.LoanAmount, interestRate, loanDto.RepaymentPeriod);

        //        if (account.BalanceAmount < result.DepositAmount)
        //            return BadRequest(new
        //            {
        //                StatusCode = 400,
        //                message = $"Your wallet balance is too low. Please deposit at least {result.DepositAmount:C} (5% of the requested loan amount) to proceed with the loan application."
        //            });

        //        // Begin transaction
        //        using var transaction = await _unitOfWork.BeginTransactionAsync();

        //        try
        //        {
        //            // Deduct deposit from account
        //            account.BalanceAmount -= result.DepositAmount;
        //            await _unitOfWork.Account.UpdateAsync(account);

        //            var loanApplication = new LoanApplication
        //            {
        //                CustomerID = loanDto.CustomerID,
        //                PlanID = loanDto.PlanID,
        //                LoanAmount = loanDto.LoanAmount,
        //                RepaymentPeriod = loanDto.RepaymentPeriod,
        //                PurposeOfLoan = loanDto.PurposeOfLoan,
        //                HasExistingLoans = false,
        //                ExistingLoanAmount = 0,
        //                LenderName = "Upstartloan",
        //                MonthlyInstallments = result.MonthlyInstallment,
        //                Status = 0,
        //                ApplicationDate = loanDto.ApplicationDate,
        //                PayMethodID = loanDto.PayMethodID,
        //                LateCharge = result.LateCharge,
        //                DepositAmount = result.DepositAmount,
        //            };

        //            await _unitOfWork.LoanApplication.AddAsync(loanApplication);

        //            await _unitOfWork.Save(); // Save both operations

        //            await transaction.CommitAsync(); // Commit transaction

        //            return Ok(new { StatusCode = 200, message = "Loan application created successfully." });
        //        }
        //        catch (Exception)
        //        {
        //            await transaction.RollbackAsync(); // Rollback on error
        //            throw;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}


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
                    return BadRequest(new { StatusCode = 400, message = "Account not created for this customer." });

                var loanPlan = await _unitOfWork.LoanPlan.GetByIdAsync(loanDto.PlanID);
                if (loanPlan == null)
                    return BadRequest(new { StatusCode = 400, message = "Invalid Loan Plan ID" });

                decimal interestRate = loanPlan.InterestRate;

                var result = await _unitOfWork.LoanApplication.CalculateEMIAsync(
                    loanDto.LoanAmount, interestRate, loanDto.RepaymentPeriod, loanPlan.ProcessingFee, loanPlan.LatePaymentPenalty);

                if (account.BalanceAmount < result.DepositAmount)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        message = $"Your wallet balance is too low. Please deposit at least {result.DepositAmount:C} (5% of the requested loan amount) to proceed with the loan application."
                    });
                }

                using var transaction = await _unitOfWork.BeginTransactionAsync();

                try
                {
                    // ✅ Deduct deposit from account
                    account.BalanceAmount -= result.DepositAmount;
                    await _unitOfWork.Account.UpdateAsync(account);

                    // ✅ Record deposit deduction in transaction table
                    var depositTransaction = new Transaction
                    {
                        CustomerId = loanDto.CustomerID,
                        TransactionType = 9, // 9 = Deposit deduction for loan application
                        Amount = result.DepositAmount,
                        TransactionDate = DateTime.UtcNow,
                        PaytMethodID = loanDto.PayMethodID,
                        Remarks = "Deposit amount deducted for loan application"
                    };
                    await _unitOfWork.Transction.AddAsync(depositTransaction);

                    // Save loan application
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
                        Status = 0, // Pending
                        ApplicationDate = loanDto.ApplicationDate,
                        PayMethodID = loanDto.PayMethodID,
                        LateCharge = result.LateCharge,
                        DepositAmount = result.DepositAmount,
                        ApplyedBy = loanDto.UserId,
                    };

                    await _unitOfWork.LoanApplication.AddAsync(loanApplication);
                    await _unitOfWork.Save();

                    await transaction.CommitAsync();

                    return Ok(new { StatusCode = 200, message = "Loan application created successfully." });
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
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
           loanDto.LoanAmount, interestRate, loanDto.RepaymentPeriod, loanPlan.ProcessingFee, loanPlan.LatePaymentPenalty);

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
                existingLoan.UpdatedBy = loanDto.UserId;
                existingLoan.UpdatedAt = DateTime.Now;
                

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
