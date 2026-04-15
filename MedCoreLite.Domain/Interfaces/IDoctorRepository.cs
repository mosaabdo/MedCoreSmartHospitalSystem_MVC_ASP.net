using MedCore.Domain.Entities;

namespace MedCore.Domain.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        //Task AddAsync(Doctor doctor);
        //Task<IEnumerable<Doctor>> GetAllAsync();
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(int specialtyId);
        Task<bool> HasAppointmentsAsync(int doctorId);
        //Task<int> GetCountAsync();
        Task<IEnumerable<Specialty>> GetSpecialtiesAsync();
        Task<Doctor?> GetDoctorDetailsAsync(int id);
        //Task SaveChangesAsync();
    }
}
