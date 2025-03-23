using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Implementation;
using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustommerController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public CustommerController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("custommer/{id}")]
        public async Task<IActionResult> GetCustommerById(int id)
        {
            try
            {
                string cacheKey = $"custommer_{id}";

                if (!_cache.TryGetValue(cacheKey, out CustommerPersonnelInfo cachedResult))
                {
                    var result = await _unitOfWork.Custommer.GetByIdAsync(id);

                    if (result == null)
                    {
                        return NotFound(new { StatusCode = 404, message = "Customer not found!" });
                    }

                    // Cache the result for future requests
                    _cache.Set(cacheKey, result, TimeSpan.FromMinutes(1));

                    return Ok(new { StatusCode = 200, message = "Success", data = result });
                }
                else
                {
                    return Ok(new { StatusCode = 200, message = "Success", data = cachedResult });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }


        [HttpGet]
        [Route("custommers")]
        public async Task<IActionResult> GetCustommers()
        {
            try
            {
                string cacheKey = $"custommers";


                if (!_cache.TryGetValue(cacheKey, out List<CustommerPersonnelInfo> cachedResult))
                {

                    var result = await _unitOfWork.Custommer.GetAllWithDetailsAsync();


                    _cache.Set(cacheKey, result, TimeSpan.FromMinutes(1));

                    return Ok(new { StatusCode = 200, message = "Success", data = result });
                }
                else
                {

                    return Ok(new { StatusCode = 200, message = "Success", data = cachedResult });
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { StatusCode = 404, message = "custommers not found!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> PostCustomer([FromBody] CustommerPersonnelInfoDTO customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    return BadRequest("Customer personnel information cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int userId = 1;

                string result = null;
                string resultSignature = null;
                string CompanyId = "0001";
                // Process Employee Image if available
                if (customerDto.CustommerImage != null && customerDto.CustommerImage.Any())
                {
                    string DocumentType = "CustommerImage";
                    result = await _unitOfWork.Custommer.SaveDocumentsListsAsync(
                        customerDto.CustommerImage,
                        customerDto.CustCardNo,
                        CompanyId,
                        DocumentType
                    );
                }

                // Process Signature Image if available
                if (customerDto.CustommerSignature != null && customerDto.CustommerSignature.Any())
                {
                    string DocumentTypeSigImage = "CustommerSignature";
                    resultSignature = await _unitOfWork.Custommer.SaveDocumentsListsAsync(
                        customerDto.CustommerSignature,
                        customerDto.CustCardNo,
                        CompanyId,
                        DocumentTypeSigImage
                    );
                }

                var customer = new CustommerPersonnelInfo
                {
                    CustCardNo = customerDto.CustCardNo,
                    CustommerImage = result,
                    CustommerSignature = resultSignature,
                    CompanyId = customerDto.CompanyId,
                    FullName = customerDto.FullName,
                    Gender = customerDto.Gender,
                    DateOfBirth = customerDto.DateOfBirth,
                    Nationality = customerDto.Nationality,
                    MaritalStatus = customerDto.MaritalStatus,
                    Occupation = customerDto.Occupation,
                    DrivingLicenseNumber = customerDto.DrivingLicenseNumber,
                    NationalIDOrPassport = customerDto.NationalIDOrPassport,
                    TaxIdentificationNumber = customerDto.TaxIdentificationNumber,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    IsActive = false
                };

                // Add the customer to the database
                await _unitOfWork.Custommer.AddAsync(customer);
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Customer personnel information created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustommerPersonnelInfoDTO customerDto)
        {
            try
            {
                if (customerDto == null)
                {
                    return BadRequest("Customer personnel information cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingCustomer = await _unitOfWork.Custommer.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound("Customer personnel information not found.");
                }

                existingCustomer.CustCardNo = customerDto.CustCardNo;
                existingCustomer.CompanyId = customerDto.CompanyId;
                existingCustomer.FullName = customerDto.FullName;
                existingCustomer.Gender = customerDto.Gender;
                existingCustomer.DateOfBirth = customerDto.DateOfBirth;
                existingCustomer.Nationality = customerDto.Nationality;
                existingCustomer.MaritalStatus = customerDto.MaritalStatus;
                existingCustomer.Occupation = customerDto.Occupation;
                existingCustomer.UpdatedAt = DateTime.Now;
                existingCustomer.UpdatedBy = 1; 

                await _unitOfWork.Custommer.UpdateAsync(existingCustomer);
                await _unitOfWork.Save();
                return Ok(new { StatusCode = 200, message = "Customer personnel information updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                // Retrieve the existing customer personnel info by ID
                var existingCustomer = await _unitOfWork.Custommer.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound("Customer personnel information not found.");
                }

                // Call DeleteAsync to remove the record from the database
                await _unitOfWork.Custommer.DeleteAsync(id);
                await _unitOfWork.Save();
                return Ok(new { StatusCode = 200, message = "Customer personnel information deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
