using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustommerController : ControllerBase
    {
        // private readonly IUserRepository _userRepository;

        public CustommerController()
        {
            
            //_unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetCustommer")]
        public async Task<IActionResult> GetCustommer()
        {
            return Ok("GetCustommer Done");
        }
    }
}
