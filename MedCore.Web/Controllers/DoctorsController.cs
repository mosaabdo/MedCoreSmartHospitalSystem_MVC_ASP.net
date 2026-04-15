using MedCore.Application.Services;
using MedCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedCore.Web.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ISpecialtyService _specialtyService;
        public DoctorsController(IDoctorService doctorService,
            ISpecialtyService specialtyService)
        {
            _doctorService = doctorService;
            _specialtyService = specialtyService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctorsBySpecialty(int specialtyId)
        {
            var doctors = await _doctorService.GetDoctorsBySpecialtyAsync(specialtyId);

            return Json(doctors);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var specialties = await _doctorService.GetSpecialtiesAsync();
            ViewBag.Specialties = new SelectList(specialties, "Id", "Name");

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(DoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var specialties = await _doctorService.GetSpecialtiesAsync();
                ViewBag.Specialties = new SelectList(specialties, "Id", "Name");
                return View(model);
            }

            await _doctorService.CreateDoctorAsync(model.Name, model.Phone, model.SpecialtyId);

            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();

            var model = new DoctorViewModel
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Phone = doctor.Phone,
                SpecialtyId = doctor.SpecialtyId
            };

            var specialties = await _specialtyService.GetAllSpecialtiesAsync();
            ViewBag.Specialties = new SelectList(specialties, "Id", "Name", doctor.SpecialtyId);

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(DoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var specialties = await _specialtyService.GetAllSpecialtiesAsync();
                ViewBag.Specialties = new SelectList(specialties, "Id", "Name", model.SpecialtyId);
                return View(model);
            }

            var result = await _doctorService.UpdateDoctorAsync(model.Id, model.Name, model.Phone, model.SpecialtyId);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
