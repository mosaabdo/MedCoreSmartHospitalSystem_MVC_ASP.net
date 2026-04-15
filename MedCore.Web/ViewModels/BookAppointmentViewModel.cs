using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.ViewModels
{
    public class BookAppointmentViewModel
    {
        [Required(ErrorMessage = "Please select a doctor")]
        [Display(Name = "Attending Doctor")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please select a patient")]
        [Display(Name = "Patient Name")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please select an appointment date and time")]
        [Display(Name = "Appointment Date & Time")]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        [Display(Name = "Additional Notes")]
        public string? Notes { get; set; }

        [Display(Name = "Chronic diseases")]
        public string? ChronicConditions { get; set; }

    }
}
