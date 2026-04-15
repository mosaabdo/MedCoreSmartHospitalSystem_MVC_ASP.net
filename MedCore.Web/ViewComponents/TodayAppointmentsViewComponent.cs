using MedCore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedCore.Web.ViewComponents
{
    public class TodayAppointmentsViewComponent : ViewComponent
    {
        private readonly IAppointmentService _appointmentService;

        public TodayAppointmentsViewComponent(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()

        {
            int count = await _appointmentService.GetTodayAppointmentsCountAsync();
            return View(count);
        }
    }
}
