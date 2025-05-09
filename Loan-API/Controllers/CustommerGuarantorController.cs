using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Implementation;
using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustommerGuarantorController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public CustommerGuarantorController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }




        [HttpGet]
        [Route("Guarantor/{id}")]
        public async Task<IActionResult> GetGuarantorById(int id)
        {
            try
            {
                // Retrieve the guarantor by ID from the unit of work
                var result = await _unitOfWork.Guarantor.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Guarantor not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomerGuarantor([FromBody] CustommerGuarantorDetailsDTO guarantorDto)
        {
            try
            {
                if (guarantorDto == null)
                {
                    return BadRequest("Customer guarantor details cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingGuarantor = await _unitOfWork.Guarantor.GetByIdAsync(guarantorDto.CustomerID);

                if (existingGuarantor == null)
                {
                    return NotFound("Customer guarantor details not found.");
                }
                existingGuarantor.GuarantorImage = guarantorDto.GuarantorImage;
                existingGuarantor.GuarantorFullName = guarantorDto.GuarantorFullName;
                existingGuarantor.RelationshipWithApplicant = guarantorDto.RelationshipWithApplicant;
                existingGuarantor.GuarantorContactNumber = guarantorDto.GuarantorContactNumber;
                existingGuarantor.GuarantorAddress = guarantorDto.GuarantorAddress;
                existingGuarantor.GuarantorNationalIDOrPassport = guarantorDto.GuarantorNationalIDOrPassport;
                existingGuarantor.GuarantorSignature = guarantorDto.GuarantorSignature;
                await _unitOfWork.Guarantor.UpdateAsync(existingGuarantor);
                await _unitOfWork.Save();
                return Ok(new { StatusCode = 200, message = "Customer guarantor details updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomerGuarantor([FromBody] CustommerGuarantorDetailsDTO guarantorDto)
        {
            try
            {
                if (guarantorDto == null)
                {
                    return BadRequest("Customer guarantor details cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var guarantor = new CustommerGuarantorDetails
                {
                    CustomerID = guarantorDto.CustomerID,
                    GuarantorImage = guarantorDto.GuarantorImage,
                    GuarantorFullName = guarantorDto.GuarantorFullName,
                    RelationshipWithApplicant = guarantorDto.RelationshipWithApplicant,
                    GuarantorContactNumber = guarantorDto.GuarantorContactNumber,
                    GuarantorAddress = guarantorDto.GuarantorAddress,
                    GuarantorNationalIDOrPassport = guarantorDto.GuarantorNationalIDOrPassport,
                    GuarantorSignature = guarantorDto.GuarantorSignature
                };

                // Add new guarantor to the database
                await _unitOfWork.Guarantor.AddAsync(guarantor);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Customer guarantor details created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
