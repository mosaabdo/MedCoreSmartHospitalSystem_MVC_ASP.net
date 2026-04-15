using MedCore.Domain.Entities;

namespace MedCore.Domain.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {

        //Task AddAsync(Patient patient);

        Task<bool> IsPhoneExistsAsync(string phone);
        Task<bool> HasAppointmentsAsync(int patientId);
        //Task<int> GetCountAsync();
        //Task SaveChangesAsync();
    }
}
