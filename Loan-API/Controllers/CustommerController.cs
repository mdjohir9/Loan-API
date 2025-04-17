using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Implementation;
using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;

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
        [Route("custommerSummary")]
        public async Task<IActionResult> GetCustommerById()
        {
            try
            {

                var result = await _unitOfWork.Custommer.GetAllCustommerSummaryAsync();

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Customer not found!" });
                }

                // Cache the result for future requests


                return Ok(new { StatusCode = 200, message = "Success", data = result });


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("custommer/{id}")]
        public async Task<IActionResult> GetCustommerById(int id)
        {
            try
            {
                string cacheKey = $"custommer_{id}";

                    var result = await _unitOfWork.Custommer.GetByIdAsync(id);

                    if (result == null)
                    {
                        return NotFound(new { StatusCode = 404, message = "Customer not found!" });
                    }

                    // Cache the result for future requests
                   

                    return Ok(new { StatusCode = 200, message = "Success", data = result });
                
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }
        [HttpGet]
        [Route("custommerDetailes/{id?}")]
        public async Task<IActionResult> GetCustommerDetailesById(int? id)
        {
            try
            {
                string cacheKey = id.HasValue ? $"custommer_{id}" : "custommer_all";

                var result = await _unitOfWork.Custommer.GetAllWithDetailsAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Customer(s) not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
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

                int userId = customerDto.UserId ?? 0; // Convert nullable int to non-nullable

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
                    EducationLevel=customerDto.EducationLevel,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId, 
                    IsActive = false
                };

                await _unitOfWork.Custommer.AddAsync(customer);
                await _unitOfWork.Save(); 


                int newCustomerId = customer.CustomerID; // Assuming CustomerID is an identity field

                // **Step 3: Update User with the New Customer ID**
                var user = new User { UserId = userId };
                await _unitOfWork.User.UpdateAsync(user, "ReferenceID", newCustomerId.ToString()); // Ensure ReferenceID is string

                // **Step 4: Save Changes** 
                await _unitOfWork.Save();

                return Ok(new { StatusCode = 200, message = "Customer personnel information created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("create-full")]
        public async Task<IActionResult> PostFullCustomer([FromBody] CustommerSaveDTO customerDto)
        {
            try
            {
                if (customerDto == null)
                    return BadRequest("Customer information cannot be null.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                string companyId = "0001";
                string? imageResult = null;
                string? signatureResult = null;



                if (customerDto.CustommerImage != null && customerDto.CustommerImage.Any())
                {
                    string DocumentType = "CustommerImage";
                    imageResult = await _unitOfWork.Custommer.SaveDocumentsListsAsync(
                        customerDto.CustommerImage,
                    customerDto.CustCardNo,
                        companyId,
                        DocumentType
                    );
                }

                // Process Signature Image if available
                if (customerDto.CustommerSignature != null && customerDto.CustommerSignature.Any())
                {
                    string DocumentTypeSigImage = "CustommerSignature";

                    signatureResult = await _unitOfWork.Custommer.SaveDocumentsListsAsync(
                        customerDto.CustommerSignature,
                    customerDto.CustCardNo,
                        companyId,
                        DocumentTypeSigImage
                    );
                }

                // Save customer full details
                await _unitOfWork.Custommer.AddCustommerAllDataAsync(customerDto);
                await _unitOfWork.Save();

                //int newCustomerId = customer.CustomerID; // Assuming CustomerID is an identity field

                //// **Step 3: Update User with the New Customer ID**
                //var user = new User { UserId = userId };
                //await _unitOfWork.User.UpdateAsync(user, "ReferenceID", newCustomerId.ToString());
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Customer with full details created successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] PersonnelInfoUpdateDTO customerDto)
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

                existingCustomer.FullName = customerDto.FullName;
                existingCustomer.Gender = customerDto.Gender;
                existingCustomer.DateOfBirth = customerDto.DateOfBirth;
                existingCustomer.Nationality = customerDto.Nationality;
                existingCustomer.MaritalStatus = customerDto.MaritalStatus;
                existingCustomer.Occupation = customerDto.Occupation;
                existingCustomer.NationalIDOrPassport = customerDto.NationalIDOrPassport;
                existingCustomer.DrivingLicenseNumber = customerDto.DrivingLicenseNumber;
                existingCustomer.TaxIdentificationNumber = customerDto.TaxIdentificationNumber;
                existingCustomer.EducationLevel = customerDto.EducationLevel;
                existingCustomer.UpdatedAt = DateTime.Now;
                existingCustomer.UpdatedBy = customerDto.UserId; 

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
