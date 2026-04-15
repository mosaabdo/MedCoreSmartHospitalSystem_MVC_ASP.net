namespace MedCore.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
