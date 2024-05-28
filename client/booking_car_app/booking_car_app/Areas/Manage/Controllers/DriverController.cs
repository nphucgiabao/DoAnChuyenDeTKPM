using AutoMapper;
using booking_car_app.ApiServices.Booking;
using booking_car_app.ApiServices.Drivers;
using booking_car_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace booking_car_app.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class DriverController : Controller
    {
        private readonly IDriverService _driverServices;
        private readonly IMapper _mapper;
        public DriverController(IDriverService driverServices, IMapper mapper)
        {
            _driverServices = driverServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _driverServices.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(string id)
        {
            var model = new DriverViewModel();
            if (id != null)
            {

            }
            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit([FromBody]DriverViewModel model)
        {
            var entity = _mapper.Map<Entities.Driver>(model);
            return Json(await _driverServices.AddEdit(entity));
        }
        
    }
}
