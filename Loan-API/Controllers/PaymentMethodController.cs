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
    public class PaymentMethodController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        public PaymentMethodController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {

            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("activePaymentMetdods")]
        public async Task<IActionResult> GetAllActivePaymentTypes()
        {
            try
            {
                var result = await _unitOfWork.RechargePaymentMethod.GetAllActiveAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "No payment types found!" });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Payment types retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    message = "An error occurred while retrieving payment types.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("paymentMetdods")]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            try
            {
                var result = await _unitOfWork.RechargePaymentMethod.GetAllAsync();

                if (result == null || !result.Any())
                {
                    return NotFound(new { StatusCode = 404, message = "No payment types found!" });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Payment types retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    message = "An error occurred while retrieving payment types.",
                    error = ex.Message
                });
            }
        }
        [HttpGet("paymentMetdod/{id}")]
        public async Task<IActionResult> GetPaymentTypeById(int id)
        {
            try
            {
                var result = await _unitOfWork.RechargePaymentMethod.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Payment type not found!" });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Payment type retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    message = "An error occurred while retrieving the payment type.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentType([FromBody] RechargePaymentMethodDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid data!" });
                }

                var entity = new RechargePaymentMethod
                {
                    Name = dto.Name,
                    IsActive = dto.IsActive
                };

                await _unitOfWork.RechargePaymentMethod.AddAsync(entity);
                await _unitOfWork.Save();

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Recharge payment type created successfully",
                    data = entity
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    message = "An error occurred while creating the payment type.",
                    error = ex.Message
                });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePaymentType([FromBody] RechargePaymentMethodDTO dto, int id)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid data!" });
                }

                var entity = await _unitOfWork.RechargePaymentMethod.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Payment type not found!" });
                }

                entity.Name = dto.Name;
                entity.IsActive = dto.IsActive;

                _unitOfWork.RechargePaymentMethod.UpdateAsync(entity);
                await _unitOfWork.Save();

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Payment type updated successfully",
                    data = entity
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    message = "An error occurred while updating the payment type.",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePaymentType(int id)
        {
            try
            {
                var entity = await _unitOfWork.RechargePaymentMethod.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound(new { StatusCode = 404, message = "Payment type not found!" });
                }

                _unitOfWork.RechargePaymentMethod.DeleteAsync(entity.Id);
                await _unitOfWork.Save();

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Payment type deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    message = "An error occurred while deleting the payment type.",
                    error = ex.Message
                });
            }
        }

    }
}
