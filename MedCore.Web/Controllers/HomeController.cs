using MedCore.Application.Services;
using MedCore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedCore.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public HomeController(IDoctorService doctorService,
            IPatientService patientService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentService = appointmentService;
        }
        public async Task<IActionResult> Index()
        {
            // سنفترض وجود دوال Count في الخدمات التي أنشأناها
            ViewBag.DoctorsCount = await _doctorService.GetCountAsync();
            ViewBag.PatientsCount = await _patientService.GetCountAsync();
            ViewBag.AppointmentsCount = await _appointmentService.GetCountAsync();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
