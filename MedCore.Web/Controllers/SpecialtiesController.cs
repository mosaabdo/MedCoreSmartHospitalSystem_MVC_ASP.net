using MedCore.Application.Services;
using MedCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedCore.Web.Controllers
{
    [Authorize]

    public class SpecialtiesController : Controller
    {
        private readonly ISpecialtyService _specialtyService;
        public SpecialtiesController(ISpecialtyService specialtyService) => _specialtyService = specialtyService;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var specialties = await _specialtyService.GetAllSpecialtiesAsync();
            return View(specialties);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var specialty = await _specialtyService.GetSpecialtyByIdAsync(id);
            if (specialty == null) return NotFound();
            return View(specialty);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create() => View();
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(SpecialtyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _specialtyService.CreateSpecialtyAsync(model.Name);

            if (result.IsSuccess)
            {
                return RedirectToAction("Create", "Doctors");
            }
            else
            {
                ModelState.AddModelError("Name", result.Message);
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var specialty = await _specialtyService.GetSpecialtyByIdAsync(id);
            if (specialty == null) return NotFound();

            return View(new SpecialtyViewModel { Id = specialty.Id, Name = specialty.Name });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(SpecialtyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _specialtyService.UpdateSpecialtyAsync(model.Id, model.Name);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _specialtyService.DeleteSpecialtyAsync(id);
            if (result.IsSuccess) TempData["SuccessMessage"] = result.Message;
            else TempData["ErrorMessage"] = result.Message;

            return RedirectToAction("Index");
        }
    }
}
