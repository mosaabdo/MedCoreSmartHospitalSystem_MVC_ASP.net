using MedCore.Domain.Entities;

namespace MedCore.Domain.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<bool> IsAppointmentExistsAsync(int doctorId,
            DateTime appointmentDate);
        Task<Appointment?> GetAppointmentWithDetailsAsync(int id);
        Task<List<Doctor>> GetDoctorsAsync();
        //Task<int> GetCountAsync();
        Task<List<Patient>> GetPatientsAsync();
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
        Task<bool> IsTimeSlotAvailableAsync(int doctorId, DateTime appointmentDate);
        Task<int> GetTodayAppointmentsCountAsync();
        //Task AddAsync(Appointment appointment);
        //Task SaveChangesAsync();
    }
}
