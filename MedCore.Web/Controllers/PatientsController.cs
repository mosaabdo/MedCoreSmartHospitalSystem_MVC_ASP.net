using MedCore.Application.Services;
using MedCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedCore.Web.Controllers
{
    [Authorize]

    public class PatientsController : Controller
    {

        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return View(patients);
        }
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet]
        public IActionResult Create() => View();
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<IActionResult> Create(PatientViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _patientService.RegisterPatientAsync(model.Name, model.Phone);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("Phone", result.Message);
            return View(model);
        }
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();

            var model = new PatientEditViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Phone = patient.Phone
            };

            return View(model);
        }
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<IActionResult> Edit(PatientEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _patientService.UpdatePatientAsync(model.Id, model.Name, model.Phone);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("Phone", result.Message);
            return View(model);
        }
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patientService.DeletePatientAsync(id);

            if (result.IsSuccess) TempData["SuccessMessage"] = result.Message;
            else TempData["ErrorMessage"] = result.Message;

            return RedirectToAction("Index");
        }
    }
}
