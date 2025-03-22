using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Implementation;
using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustommerEmploymentController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public CustommerEmploymentController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("employment/{id}")]
        public async Task<IActionResult> GetEmploymentById(int id)
        {
            try
            {
                // Retrieve the contact by ID from the unit of work
                var result = await _unitOfWork.Contact.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Customer employment not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> PostCustomerEmployment([FromBody] CustommerEmploymentDTO employmentDto)
        {
            try
            {
                if (employmentDto == null)
                {
                    return BadRequest("Customer employment details cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                 
                var employment = new CustommerEmployment
                {
                    CustomerID = employmentDto.CustomerID,
                    EmploymentType = employmentDto.EmploymentType,
                    EmployerOrBusnName = employmentDto.EmployerOrBusnName,
                    JobTitleOrBusnType = employmentDto.JobTitleOrBusnType,
                    MonthlyIncOrBusnRev = employmentDto.MonthlyIncOrBusnRev,
                    YearsOfExpOrBusnAge = employmentDto.YearsOfExpOrBusnAge,
                    WorkOrBusnAddress = employmentDto.WorkOrBusnAddress,
                    EmployerOrBusnContact = employmentDto.EmployerOrBusnContact
                };

                await _unitOfWork.Employment.AddAsync(employment);
                await _unitOfWork.Save();
                return Ok(new { StatusCode = 200, message = "Customer employment details saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomerEmployment([FromBody] CustommerEmploymentDTO employmentDto)
        {
            try
            {
                if (employmentDto == null)
                {
                    return BadRequest("Customer employment details cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingEmployment = await _unitOfWork.Employment.GetByIdAsync(employmentDto.CustomerID);

                if (existingEmployment == null)
                {
                    return NotFound("Customer employment details not found.");
                }

                existingEmployment.EmploymentType = employmentDto.EmploymentType;
                existingEmployment.EmployerOrBusnName = employmentDto.EmployerOrBusnName;
                existingEmployment.JobTitleOrBusnType = employmentDto.JobTitleOrBusnType;
                existingEmployment.MonthlyIncOrBusnRev = employmentDto.MonthlyIncOrBusnRev;
                existingEmployment.YearsOfExpOrBusnAge = employmentDto.YearsOfExpOrBusnAge;
                existingEmployment.WorkOrBusnAddress = employmentDto.WorkOrBusnAddress;
                existingEmployment.EmployerOrBusnContact = employmentDto.EmployerOrBusnContact;
                await _unitOfWork.Employment.UpdateAsync(existingEmployment);
                await _unitOfWork.Save();
                return Ok(new { StatusCode = 200, message = "Customer employment details updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
