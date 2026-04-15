namespace MedCore.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Price { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
