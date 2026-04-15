using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.ViewModels
{
    public class PatientEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient name is required")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;
    }
}
