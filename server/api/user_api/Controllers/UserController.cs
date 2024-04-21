using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using user_api.Customs;
using user_api.Identity;
using user_api.Models;

namespace user_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore)
        {
            _logger = logger;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<ApplicationUser>)_userStore;
        }
        //
        [HttpGet]
        [IdentityServerAuthorize]
        public IActionResult GetUsers()
        {
            return Ok(new UserModel { PhoneNumber = "123456" });
        }

        [HttpPost]
        //[Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {

            if (ModelState.IsValid)
            {
                var newUser = Activator.CreateInstance<ApplicationUser>();
                newUser.FullName = model.FullName;
                newUser.PhoneNumber = model.PhoneNumber;
                await _userStore.SetUserNameAsync(newUser, model.PhoneNumber, CancellationToken.None);
                await _emailStore.SetEmailAsync(newUser, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, model.Role);
                    _logger.LogInformation("User created a new account with password.");
                    return Ok(new ResponseModel { Success = true, Message = AppMessage.Save.Success });
                }
            }
            return BadRequest(new ResponseModel { Success = false, Message = AppMessage.Save.Error });
        }
        [IdentityServerAuthorize]
        [HttpPost]
        //[Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                _logger.LogInformation("User not found.");
                return BadRequest(new ResponseModel { Success = false, Message = "Not found user" });
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (result.Succeeded)
            {
                return Ok(new ResponseModel { Success = true, Message = AppMessage.Save.Success });
            }
            _logger.LogInformation("Reset password error.");
            return BadRequest(new ResponseModel { Success = false, Message = AppMessage.Save.Error });
        }
    }
}
