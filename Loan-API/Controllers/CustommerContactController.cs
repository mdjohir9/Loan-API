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
    public class CustommerContactController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public CustommerContactController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }




        [HttpGet]
        [Route("contact/{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            try
            {
                // Retrieve the contact by ID from the unit of work
                var result = await _unitOfWork.Contact.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Customer contact not found!" });
                }

                return Ok(new { StatusCode = 200, message = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }




        [HttpPost("create")]
        public async Task<IActionResult> PostCustomer([FromBody] CustommerContactDTO contactDto)
        {
            try
            {
                if (contactDto == null)
                {
                    return BadRequest("Customer contact details cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var contact = new CustommerContact
                {
                    CustomerID = contactDto.CustomerID,
                    PhoneNumber = contactDto.PhoneNumber,
                    AlternativePhoneNumber = contactDto.AlternativePhoneNumber,
                    EmailAddress = contactDto.EmailAddress,
                    PreStreet = contactDto.PreStreet,
                    PerStreet = contactDto.PerStreet,
                    PreZIP = contactDto.PreZIP,
                    PerZIP = contactDto.PerZIP,
                    PreCity = contactDto.PreCity,
                    PerCity = contactDto.PerCity,
                    PreState = contactDto.PreState,
                    PerState = contactDto.PerState
                };

                await _unitOfWork.Contact.AddAsync(contact);
                await _unitOfWork.Save();
                return Ok(new { StatusCode = 200, message = "Customer contact saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustommerContactDTO contactDto)
        {
            try
            {
                if (contactDto == null)
                {
                    return BadRequest("Customer contact details cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var existingContact = await _unitOfWork.Contact.GetByIdAsync(contactDto.CustomerID);

                if (existingContact == null)
                {
                    return NotFound("Customer contact details not found.");
                }

                existingContact.PhoneNumber = contactDto.PhoneNumber;
                existingContact.AlternativePhoneNumber = contactDto.AlternativePhoneNumber;
                existingContact.EmailAddress = contactDto.EmailAddress;
                existingContact.PreStreet = contactDto.PreStreet;
                existingContact.PerStreet = contactDto.PerStreet;
                existingContact.PreZIP = contactDto.PreZIP;
                existingContact.PerZIP = contactDto.PerZIP;
                existingContact.PreCity = contactDto.PreCity;
                existingContact.PerCity = contactDto.PerCity;
                existingContact.PreState = contactDto.PreState;
                existingContact.PerState = contactDto.PerState;

                await _unitOfWork.Contact.UpdateAsync(existingContact);
                await _unitOfWork.Save();
                return Ok(new { StatusCode = 200, message = "Customer contact updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
