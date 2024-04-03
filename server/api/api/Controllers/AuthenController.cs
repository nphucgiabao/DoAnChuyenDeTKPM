using api.Identity;
using api.Models;
using api.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly string _secretKey;
        public AuthenController(SignInManager<AppUser> signInManager, IOptions<AppSettings> config)
        {
            _signInManager = signInManager;           
            _secretKey = config.Value.SecretKey;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var login = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);           
            if (login.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(model.UserName);
                return Ok(new ApiResponse() {
                    Success = true,
                    Message = "Authenticate success",
                    Data = AppHelper.GenerateToken(user, _secretKey)
                });
            }
            return Ok(new ApiResponse() { Success = false, Message = "Invalid Username/Password" });
        }
    }
}
