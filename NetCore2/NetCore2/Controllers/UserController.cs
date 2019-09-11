using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCore2.Models;

namespace NetCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        AuthDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentRole> _roleManager;
        private SignInManager<AppUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        


        public UserController(UserManager<AppUser> userManager, AuthDbContext context, RoleManager<IdentRole> roleManager, SignInManager<AppUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;

            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<Object> RegisterUser(AppUserModel model)
        {

            var applicationUser = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                DormId = model.DormId,
                Room = model.Room

            };
            Role role = new Role { Name = "User", NormalizedName = "USER" };

            if (_userManager.Users.SingleOrDefault(u => u.UserName == model.UserName) == null)
            {

                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                if (model.Role != null) await _userManager.AddToRoleAsync(applicationUser, model.Role.Name);
                else await _userManager.AddToRoleAsync(applicationUser, role.Name);
                if (result.Succeeded)
                {
                    AppUser appUser = _userManager.Users.SingleOrDefault(u => u.UserName == model.UserName);
                    appUser.EmailConfirmed = true;
                    await _userManager.UpdateAsync(appUser);

                }
                return result;
            }
            else
            {
                return BadRequest();
            }

        }





        [HttpGet]
        [Route("UserProfile")]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var userRole = await _userManager.GetRolesAsync(user);
            return new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.UserName,
                user.Room,
                user.DormId,
                role = userRole.FirstOrDefault(),

            };
        }

        [HttpGet]
        public async Task<Object> GetUsersAsync()
        {

            var allusers = _userManager.Users.ToArray();
            var allusersModel = new List<AppUserModel>();
            foreach (AppUser user in allusers)
            {

                var UserRoleNames = await _userManager.GetRolesAsync(user);
                var role = _roleManager.Roles.Single(r => r.Name == UserRoleNames.First());

                allusersModel.Add(new AppUserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = "",
                    FullName = user.FullName,
                    Role = role,
                    Room = user.Room,
                    DormId = user.DormId

                });


            }
            if (User.IsInRole("Admin")) { return allusersModel; }
            else
            {
                return allusersModel.Where(u => u.Role.Name == "Executor");
            }


        }





        [HttpDelete("{id}")]
        public async Task<Object> DeleteUser(string id)
        {

            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);
            IList<string> role;
            string rolestr = null;
            try
            {
                role = await _userManager.GetRolesAsync(user);
                rolestr = role.ToList().FirstOrDefault();
            }
            catch (ArgumentNullException ex)
            {

            }

            if (user != null && rolestr != null)
            {
                var remFromRole = await _userManager.RemoveFromRoleAsync(user, rolestr);
                if (remFromRole.Succeeded)
                {
                    var results = await _userManager.DeleteAsync(user);
                    if (results.Succeeded)
                    {
                        return Ok(results);
                    }
                    else return ":(";

                }

            }
            else
            {
                if (user != null)
                {
                    var results = await _userManager.DeleteAsync(user);
                    if (results.Succeeded)
                    {
                        return Ok();
                    }
                    else return "Не удалось удалить пользователя";
                }
            }
            return "Такого пользователя не существует";
        }

        [HttpPut]
        public async Task<Object> UpdateUser(AppUserModel model)
        {

            var user = _userManager.Users.SingleOrDefault(us => us.Id == model.Id);
           await _userManager.UpdateSecurityStampAsync(user);
            if (user != null)
            {
                if (model.UserName != "") user.UserName = model.UserName;
                if (model.Email != "") user.Email = model.Email;
                if (model.FullName != "") user.FullName = model.FullName;
                user.Room = model.Room;
                user.DormId = model.DormId;
                if (model.Password != "") user.PasswordHash =
                 _userManager.PasswordHasher.HashPassword(user, model.Password);
                if (await _userManager.IsInRoleAsync(user, model.Role.Name))
                {

                }
                else
                {
                    var userRole = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRoleAsync(user, userRole.FirstOrDefault());
                    await _userManager.AddToRoleAsync(user, model.Role.Name);
                }
                try
                {
                    var result = await _userManager.UpdateAsync(user);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("executors")]
        async public Task<Object> getExecutors()
        {

            return await _userManager.GetUsersInRoleAsync("EXECUTOR");
        }


        [HttpGet]
        [Route("Roles")]
        public Object GetRoles()
        {
            if (User.IsInRole("Admin"))
            {
                return _roleManager.Roles.ToArray();
            }
            else
            {
                if (User.IsInRole("Operator")) return _roleManager.Roles.ToArray().TakeLast(2);
                else return _roleManager.Roles.ToArray().Where(r => r.Name == "User");
            }
            return Unauthorized();

        }

    }

}
