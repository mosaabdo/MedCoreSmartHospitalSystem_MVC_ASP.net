using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.ViewModels
{
    public class SpecialtyViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Specialty name is required")]
        public string Name { get; set; } = string.Empty;
    }
}
