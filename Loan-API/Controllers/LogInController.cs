using Loan_API.DTO;
using Loan_API.DTO.users;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Loan_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly IUnitOfWork _unitOfWork;
        int userId = 1;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogInController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMemoryCache cache, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _cache = cache;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult PostUsers([FromBody] LoginDTO loginDTO)
        {
            try
            {

                if (loginDTO == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "User object is null." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { StatusCode = 400, message = "Invalid model state.", data = ModelState });
                }

                var users = _unitOfWork.Login.GetLoginInfo(loginDTO.UserName, loginDTO.UserPassword);
                var _user = users.FirstOrDefault();

                if (_user == null)
                {
                    return NotFound(new { StatusCode = 404, message = "User not found or invalid credentials." });
                }

                var userRole = _unitOfWork.Login.GetUserProfileInfo(_user.UserRoleID);
                var _userRoles = users.FirstOrDefault();
                var Company = _unitOfWork.Login.GetUserCompany(_user.UserId);
                var loan = !string.IsNullOrWhiteSpace(_user.ReferenceID)
                    ? _unitOfWork.Login.GetLoanInformation(Convert.ToInt32(_user.ReferenceID))
                    : null;
         
                if (Company != null)
                {
                    switch (Company.Status)
                    {
                        case 0:
                            return BadRequest(new { StatusCode = 400, message = "Your company is inactive." });
                        case 2:
                            return BadRequest(new { StatusCode = 400, message = "Your company is expired." });
                        case 3:
                            return BadRequest(new { StatusCode = 400, message = "Your company is suspended." });
                    }
                }

                var accessToken = _unitOfWork.Login.GenerateJwtToken(_user);

            

                if (_user.IsAdministrator == null)
                {
                    _user.IsAdministrator = false;
                }

                //var request = _httpContextAccessor.HttpContext.Request;
                //var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

                //var imageUrl = string.IsNullOrEmpty(_user.UserImage) ? "" : $"{baseUrl}/{_user.UserImage}";

                var userinfo = users
                    .Select(u => new LoginInfoDTO
                    {
                        UserId = u.UserId,
                        CompanyId = u.CompanyId,
                        CompanyName = Company?.CompanyName,
                        Status = Company?.Status,
                        UserName = ComplexScriptingSystem.ComplexLetters.getEntangledLetters(u.UserName),
                        UserPassword = ComplexScriptingSystem.ComplexLetters.getEntangledLetters(u.UserPassword),
                        UserImage = u.UserImage,
                        Name = (u.FirstName + " " + u.LastName),
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserRoleID = u.UserRoleID,  
                        RoleName = userRole.UserRoleName,
                        Email = u.Email,
                        IsGuestUser = u.IsGuestUser,
                        CustomerID = u.ReferenceID,
                        AdditionalPermissions = u.AdditionalPermissions,
                        RemovedPermissions = u.RemovedPermissions,
                        IsAdministrator = u.IsAdministrator,
                        dataAccessLevel=userRole.DataAccessLevel.ToString(),
                        LoanId=loan?.LoanID,
                    })
                    .FirstOrDefault();

                if (userinfo == null)
                {
                    return NotFound(new { StatusCode = 404, message = "User information not found." });
                }

                HttpContext.Session.SetString("UserId", _user.UserId.ToString());
                HttpContext.Session.SetString("UserName", _user.UserName);

                return Ok(new
                {
                    StatusCode = 200,
                    message = "Login successful.",
                    data = userinfo,
                    AccessToken = accessToken,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred.", error = ex.Message });
            }
        }


        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> PostUsers([FromBody] RegistrationDTO usersDTO)
        {
            try
            {
                // Check if the incoming user object is null
                if (usersDTO == null)
                {
                    return BadRequest(new { StatusCode = 400, message = "User object is null." });
                }
                if (usersDTO.NewPassword == usersDTO.ConfirmPassword)
                {
                    bool userNameExists = await _unitOfWork.User.CheckUserNameIsExist(usersDTO.EamilOrPhone);
                    if (userNameExists)
                    {
                        return Ok(new { StatusCode = 400, message = "The Phone Or Email already exists. Please choose a different Email or Phone Number." });
                    }
                }
                else
                {
                    return BadRequest(new { StatusCode = 400, message = "New Password and Confirm Passward Not Metch" });
                }
                // Ensure that the username does not already exist in the database
         

                // Validate the model state
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userRole = _unitOfWork.Login.GetUserRoleByDataAccessLevel(1);

                if (userRole == null || userRole.UserRoleId == 0) 
                {
                    return BadRequest(new { StatusCode = 400, message = "Please set up UserRole" });
                }

                var userRoleId = userRole.UserRoleId;


                // Proceed to create a new User
                var user = new User
                {
                    FirstName = usersDTO.FirstName,
                    LastName = usersDTO.LastName,
                    UserName = ComplexScriptingSystem.ComplexLetters.getTangledLetters(usersDTO.EamilOrPhone),
                    UserPassword = ComplexScriptingSystem.ComplexLetters.getTangledLetters(usersDTO.ConfirmPassword),
                    Email = usersDTO.EamilOrPhone,
                    UserRoleID = userRoleId,
                    IsGuestUser = true,
                    IsApprovingAuthority = false,
                    ReferenceID = null,
                    AdditionalPermissions = null,
                    RemovedPermissions = null,
                    DataAccessPermission = null,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    CompanyId = "1111"
                };

                // Add the new user and save changes
                await _unitOfWork.User.AddAsync(user);
                await _unitOfWork.Save();

                // Clear the cache for user-related data if necessary
                string cacheKey = $"users";
                _cache.Remove(cacheKey);

                // Return success response
                return Ok(new { StatusCode = 200, message = "User created successfully" });
            }
            catch (Exception ex)
            {
                // Return error response in case of any exceptions
                return StatusCode(500, new { StatusCode = 500, message = "An error occurred", error = ex.Message });
            }
        }
    }
}
