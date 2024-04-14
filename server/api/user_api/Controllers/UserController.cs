using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_api.Identity;
using user_api.Models;

namespace user_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            Activator.CreateInstance<ApplicationUser>();
        }
        //[HttpPost]
        //public IActionResult Register([FromBody] UserModel model)
        //{

        //    if (ModelState.IsValid)
        //    {

        //    }
        //    return new ResponseModel().
        //}

    }
}
