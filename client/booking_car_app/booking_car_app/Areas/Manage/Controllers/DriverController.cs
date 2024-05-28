using AutoMapper;
using booking_car_app.ApiServices.Booking;
using booking_car_app.ApiServices.Drivers;
using booking_car_app.ApiServices.User;
using booking_car_app.Areas.Manage.Models;
using booking_car_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace booking_car_app.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class DriverController : Controller
    {
        private readonly IDriverService _driverServices;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        public DriverController(IDriverService driverServices, IMapper mapper, IUserServices userServices)
        {
            _driverServices = driverServices;
            _mapper = mapper;
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _driverServices.GetAll();
            return Json(new { data = JsonConvert.DeserializeObject<List<Entities.Driver>>(result.Data.ToString()) });
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(Guid id)
        {
            var model = new DriverViewModel();
            if (id != Guid.Empty)
            {
                var result = await _driverServices.GetById(id);
                if (result.Success)
                    model = JsonConvert.DeserializeObject<DriverViewModel>(result.Data.ToString());
            }
            return View(model); 
        }
        [HttpGet]
        public async Task<IActionResult> CreateAccount(Guid id)
        {
            var result = await _driverServices.GetById(id);
            var account = new AccountViewModel();
            if (result.Success)
            {
                var driver = JsonConvert.DeserializeObject<Entities.Driver>(result.Data.ToString());
                account.UserName = driver.Phone;
                account.OId = id;
            }                
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit([FromBody]DriverViewModel model)
        {
            var entity = _mapper.Map<Entities.Driver>(model);
            return Json(await _driverServices.AddEdit(entity));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount([FromBody]AccountViewModel model)
        {
            var user = _mapper.Map<Entities.User>(model);
            user.Role = "Driver";
            return Json(await _userServices.Register(user));
        }

    }
}
