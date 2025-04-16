using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;
namespace Loan_API.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private static List<PermissionRouteDTO> PermissionList = new List<PermissionRouteDTO>();
        //public List<PermissionRouteDTO> PermissionList { get; set; }
        public UserRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IEnumerable<string>> GetUserRolePermissionById(int id)
        {
            var permissions = await _dbContext.UserRole
                                              .Where(ur => ur.UserRoleId == id)
                                              .Select(ur => ur.Permissions)
                                              .FirstOrDefaultAsync();

            return permissions?.Split(',').Select(p => p.Trim()) ?? Enumerable.Empty<string>();
        }
        public async Task<bool> CheckUserNameIsExist(string userName)
        {

            var existingUser = await _dbContext.Users
                                               .Where(u => u.UserName == ComplexScriptingSystem.ComplexLetters.getTangledLetters(userName) &&
                                                           (u.Deleted == false || u.Deleted == null))
                                               .FirstOrDefaultAsync();


            return existingUser != null;
        }

        public async Task<bool> CheckUserNameIsExistById(string userName, int UserId)
        {
            // Step 1: Check if the username exists for the specific UserId
            var existingUser = await _dbContext.Users
                                               .Where(u => u.UserId == UserId &&
                                                           u.UserName == ComplexScriptingSystem.ComplexLetters.getTangledLetters(userName) &&
                                                           (u.Deleted == false || u.Deleted == null))
                                               .FirstOrDefaultAsync();

            // If the user with the given UserId and username is found, return true
            if (existingUser != null)
            {
                return false;
            }

            // Step 2: If the user doesn't match, check if any other user has this username
            var otherUser = await _dbContext.Users
                                            .Where(u => u.UserId != UserId &&
                                                        u.UserName == ComplexScriptingSystem.ComplexLetters.getTangledLetters(userName) &&
                                                        (u.Deleted == false || u.Deleted == null))
                                            .FirstOrDefaultAsync();

            // Return true if any other user with the same username is found
            return otherUser != null;
        }




        public async Task<IEnumerable<object>> GetDynamicMenue(int userId)
        {
            var menu = new List<object>
            {
                new
                {
                    path = "/dashboard",
                    title = "Dashboard",
                    iconType = "nzIcon",
                    iconTheme = "outline",
                    icon = "appstore-add",
                    submenu = new List<object>()
                },
                new
                {
                    path = "",
                    title = "Employee",
                    iconType = "nzIcon",
                    iconTheme = "outline",
                    icon = "user",
                    submenu = new List<object>
                    {
                        new { path = "/employee/add", title = "Employee-Add", iconType = "", icon = "", iconTheme = "", submenu = new List<object>() },
                        new { path = "/employee/list", title = "Employee-List", iconType = "", icon = "", iconTheme = "", submenu = new List<object>() },
                        new { path = "/employee/profile", title = "Profile", iconType = "", icon = "", iconTheme = "", submenu = new List<object>() },
                        new { path = "/employee/separation-add", title = "Separation-Add", iconType = "", icon = "", iconTheme = "", submenu = new List<object>() },
                        new { path = "/employee/separation-list", title = "Separation-List", iconType = "", icon = "", iconTheme = "", submenu = new List<object>() }
                    }
                }
            };

            return await Task.FromResult(menu);
        }





 

        public async Task<IEnumerable<UsersDTO>> GetAllUsersAsync(string companyId, bool IsAdministrator)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var query = from user in _dbContext.Users
                        join userRole in _dbContext.UserRole
                        on user.UserRoleID equals userRole.UserRoleId into userRolesGroup
                        from userRole in userRolesGroup.DefaultIfEmpty()

                        where user.Deleted == false || user.Deleted == null
                        select new
                        {
                            user.UserId,
                            user.UserRoleID,
                            UserRoleName = userRole.UserRoleName,
                            user.FirstName,
                            user.LastName,
                            user.UserName,
                            user.UserImage,
                            user.UserPassword,
                            user.Email,
                            user.AdditionalPermissions,
                            user.RemovedPermissions,
                            user.DataAccessPermission,
                            userRole.DataAccessLevel,
                            user.IsActive,
                            user.ReferenceID,
                            user.IsGuestUser,
                            user.IsApprovingAuthority,
                            user.CompanyId
                        };

            if (!IsAdministrator)
            {
                query = query.Where(user => user.CompanyId == companyId);
            }

            var users = await query.ToListAsync();

            if (users == null || !users.Any())
            {
                return Enumerable.Empty<UsersDTO>();
            }

            var userDtos = users.Select(user => new UsersDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserImage = string.IsNullOrEmpty(user.UserImage) ? string.Empty : $"{baseUrl}/{user.UserImage}",
                UserRoleID = user.UserRoleID,
                UserRoleName = user.UserRoleName,
                UserPassword = user.UserPassword,
                Email = user.Email,
                ReferenceID = user.ReferenceID,
                AdditionalPermissions = user.AdditionalPermissions,
                RemovedPermissions = user.RemovedPermissions,
                DataAccessLevel = user.DataAccessLevel,
                DataAccessPermission = user.DataAccessPermission,
                IsActive = user.IsActive,
                IsGuestUser = user.IsGuestUser,
                IsApprovingAuthority = user.IsApprovingAuthority,
             
            });

            return userDtos;
        }





        //public async Task<IEnumerable<UserProfileDTO>> GetUserProfileAsync(int id, string companyId)
        //{
        //    var request = _httpContextAccessor.HttpContext.Request;
        //    var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

        //    var userQuery = from u in _dbContext.users
        //                    where u.UserId == id
        //                    join userRole in _dbContext.userRoles on u.UserRoleID equals userRole.UserRoleId into userRolesGroup
        //                    from userRole in userRolesGroup.DefaultIfEmpty()
        //                    where (u.Deleted == false || u.Deleted == null)

        //                    select new
        //                    {
        //                        u.UserId,
        //                        Name = string.IsNullOrWhiteSpace(u.FirstName + " " + u.LastName) ? null : u.FirstName + " " + u.LastName,
        //                        u.UserRoleID,
        //                        UserRoleName = userRole.UserRoleName,
        //                        u.UserName,
        //                        u.UserImage,
        //                        u.UserPassword,
        //                        u.Email,
        //                        u.AdditionalPermissions,
        //                        u.RemovedPermissions,
        //                        u.DataAccessPermission,
        //                        userRole.DataAccessLevel,
        //                        u.IsActive,
        //                        u.ReferenceID,
        //                        u.IsGuestUser,
        //                        u.IsApprovingAuthority,
        //                    };

        //    var user = await userQuery
        //        .Select(u => new
        //        {
        //            UserDetails = u,
        //            ReferenceInfo = !string.IsNullOrEmpty(u.ReferenceID) ?
        //                (from empInfo in _dbContext.PersonnelEmployeeInfos
        //                 where empInfo.EmpId == u.ReferenceID
        //                 join pecs in _dbContext.PersonnelEmpCurrentStatuses on empInfo.EmpId equals pecs.EmpId into pecsGroup
        //                 from pecs in pecsGroup.DefaultIfEmpty()
        //                 join dept in _dbContext.HrdDepartments on pecs.DptId equals dept.DptId into deptGroup
        //                 from dept in deptGroup.DefaultIfEmpty()
        //                 join grp in _dbContext.HrdGroups on pecs.Gid equals grp.Gid into grpGroup
        //                 from grp in grpGroup.DefaultIfEmpty()
        //                 join dsg in _dbContext.HrdDesignations on pecs.DsgId equals dsg.DsgId into dsgGroup
        //                 from dsg in dsgGroup.DefaultIfEmpty()
        //                 join sft in _dbContext.HrdShifts on pecs.SftId equals sft.SftId.ToString() into sftGroup
        //                 from sft in sftGroup.DefaultIfEmpty()
        //                 join adrs in _dbContext.PersonnelEmpAddresses on empInfo.EmpId equals adrs.EmpId.ToString() into adrsGroup
        //                 from adrs in adrsGroup.DefaultIfEmpty()
        //                 select new
        //                 {
        //                     empInfo.EmpName,
        //                     DeptName = dept.DptName,
        //                     DsgName = dsg.DsgName,
        //                     GroupName = grp.Gname,
        //                     ShiftName = sft.SftName,
        //                     Phone = adrs.MobileNo,
        //                     IsActive = pecs.IsActive
        //                 }).FirstOrDefault()
        //                : null
        //        })
        //        .FirstOrDefaultAsync();

        //    if (user == null)
        //    {
        //        return Enumerable.Empty<UserProfileDTO>();
        //    }

        //    List<string> departmentNames = new List<string>();
        //    List<string> departmentNamess = new List<string>();

        //    if (user.UserDetails != null && !string.IsNullOrEmpty(user.UserDetails.DataAccessPermission?.ToString()))
        //    {
        //        var dataAccessPermission = user.UserDetails.DataAccessPermission.ToString();
        //        var cleanedDataAccessPermission = dataAccessPermission.Trim('[', ']', ' ');

        //        var departmentIdsString = cleanedDataAccessPermission
        //            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        //            .Select(id => id.Trim('\"'))
        //            .ToList();
        //        var query = $"SELECT * FROM HRD_Department WHERE CompanyId = '{companyId}' AND DptId IN ({string.Join(", ", departmentIdsString.Select(id => $"'{id}'"))})";

        //        var departmentsFull = await _dbContext.HrdDepartments
        //                                              .FromSqlRaw(query)
        //                                              .ToListAsync();

        //        departmentNamess = departmentsFull
        //            .Select(d => d.DptName)
        //            .ToList();
        //    }

        //    var userProfileDto = new UserProfileDTO
        //    {
        //        UserId = user.UserDetails.UserId,
        //        Name = string.IsNullOrEmpty(user.UserDetails.Name) ? user.ReferenceInfo?.EmpName : user.UserDetails.Name,
        //        UserName = ComplexScriptingSystem.ComplexLetters.getEntangledLetters(user.UserDetails.UserName),
        //        Department = user.ReferenceInfo?.DeptName,
        //        Designation = user.ReferenceInfo?.DsgName,
        //        Group = user.ReferenceInfo?.GroupName,
        //        Shift = user.ReferenceInfo?.ShiftName,
        //        Phone = user.ReferenceInfo?.Phone,
        //        UserImage = string.IsNullOrEmpty(user.UserDetails.UserImage) ? string.Empty : $"{user.UserDetails.UserImage}",
        //        UserRoleID = user.UserDetails.UserRoleID,
        //        UserRoleName = user.UserDetails.UserRoleName,
        //        UserPassword = user.UserDetails.UserPassword,
        //        Email = user.UserDetails.Email,
        //        ReferenceID = user.UserDetails.ReferenceID,
        //        AdditionalPermissions = user.UserDetails.AdditionalPermissions,
        //        RemovedPermissions = user.UserDetails.RemovedPermissions,
        //        DataAccessLevel = user.UserDetails.DataAccessLevel,
        //        DataAccessPermission = departmentNamess,
        //        IsActive = user.UserDetails.IsActive,
        //        IsGuestUser = user.UserDetails.IsGuestUser,
        //        IsApprovingAuthority = user.UserDetails.IsApprovingAuthority,
        //    };

        //    return new List<UserProfileDTO> { userProfileDto };

        //}




    }
}
