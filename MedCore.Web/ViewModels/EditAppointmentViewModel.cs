using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.ViewModels
{
    public class EditAppointmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a patient")]
        [Display(Name = "Patient")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please select a doctor")]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please select date and time")]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        public string? Notes { get; set; }
    }
}
