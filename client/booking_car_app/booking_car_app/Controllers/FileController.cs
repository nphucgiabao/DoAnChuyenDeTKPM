using booking_car_app.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _hostingEnvironment = webHostEnvironment;
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                var path = $"{this._hostingEnvironment.WebRootPath}\\img";
                return Json(new { fileName = await AppHelper.SaveFile(path, file.FileName, file.OpenReadStream()) });
            }
            return BadRequest();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(string fileName)
        {
            var path = $"{this._hostingEnvironment.WebRootPath}\\img\\{fileName}";
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                    return Json(new { success = true, message = "Đã xóa file" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            return BadRequest();
        }
    }
}
