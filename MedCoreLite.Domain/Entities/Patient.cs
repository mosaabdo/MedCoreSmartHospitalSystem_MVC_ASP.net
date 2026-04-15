namespace MedCore.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
