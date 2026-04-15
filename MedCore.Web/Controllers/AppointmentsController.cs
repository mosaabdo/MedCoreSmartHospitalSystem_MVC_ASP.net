using MedCore.Application.Services;
using MedCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedCore.Web.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IDoctorService _doctorService;

        public AppointmentsController(IAppointmentService appointmentService,
            ISpecialtyService specialtyService,
            IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _specialtyService = specialtyService;
            _doctorService = doctorService;
        }

        // ==================== INDEX ====================
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet]
        public async Task<IActionResult> Book()
        {
            var specialties = await _specialtyService.GetAllSpecialtiesAsync();
            var patients = await _appointmentService.GetPatientsAsync();

            ViewBag.Specialties = new SelectList(specialties, "Id", "Name");
            ViewBag.Patients = new SelectList(patients, "Id", "Name");

            return View();
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<IActionResult> Book(BookAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var specialties = await _specialtyService.GetAllSpecialtiesAsync();
                var patients = await _appointmentService.GetPatientsAsync();
                ViewBag.Specialties = new SelectList(specialties, "Id", "Name");
                ViewBag.Patients = new SelectList(patients, "Id", "Name", model.PatientId);
                return View(model);
            }

            var result = await _appointmentService.BookAppointmentAsync(
                model.PatientId, model.DoctorId, model.AppointmentDate, model.Notes);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            var specs = await _specialtyService.GetAllSpecialtiesAsync();
            var pats = await _appointmentService.GetPatientsAsync();
            ViewBag.Specialties = new SelectList(specs, "Id", "Name");
            ViewBag.Patients = new SelectList(pats, "Id", "Name", model.PatientId);
            return View(model);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();

            var model = new EditAppointmentViewModel
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Notes = appointment.Notes
            };

            var patients = await _appointmentService.GetPatientsAsync();
            var doctors = await _doctorService.GetAllDoctorsAsync();

            ViewBag.Patients = new SelectList(patients, "Id", "Name", model.PatientId);
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name", model.DoctorId);

            return View(model);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var patients = await _appointmentService.GetPatientsAsync();
                var doctors = await _doctorService.GetAllDoctorsAsync();
                ViewBag.Patients = new SelectList(patients, "Id", "Name", model.PatientId);
                ViewBag.Doctors = new SelectList(doctors, "Id", "Name", model.DoctorId);
                return View(model);
            }

            var result = await _appointmentService.UpdateAppointmentAsync(
                model.Id, model.PatientId, model.DoctorId, model.AppointmentDate, model.Notes);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            var pats = await _appointmentService.GetPatientsAsync();
            var docs = await _doctorService.GetAllDoctorsAsync();
            ViewBag.Patients = new SelectList(pats, "Id", "Name", model.PatientId);
            ViewBag.Doctors = new SelectList(docs, "Id", "Name", model.DoctorId);

            return View(model);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appointmentService.CancelAppointmentAsync(id);

            if (result.IsSuccess)
                TempData["SuccessMessage"] = result.Message;
            else
                TempData["ErrorMessage"] = result.Message;

            return RedirectToAction("Index");
        }
    }
}