using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCore2.Models;

namespace NetCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : Controller
    {
        private UserManager<AppUser> _userManager;
        

        public UserProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            
        }
        [HttpGet]
        [Authorize]
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
                role = userRole.FirstOrDefault(),
                user.DormId,
                user.Room
                
            };
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web method for admin";
        }


        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("ForUser")]
        public string GetForUser()
        {
            return "Web method for user";
        }


        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        [Route("ForAdminOrUser")]
        public string GetAdminOrUser()
        {
            return "Web method for AdminOrUser";
        }

    }
}