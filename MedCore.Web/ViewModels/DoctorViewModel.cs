using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Doctor name is required")]
        [Display(Name = "Doctor Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a specialty")]
        [Display(Name = "Specialty")]
        public int SpecialtyId { get; set; }
    }
}
