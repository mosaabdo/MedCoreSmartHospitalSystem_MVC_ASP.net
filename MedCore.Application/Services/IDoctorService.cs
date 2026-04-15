using MedCore.Application.Common;
using MedCore.Domain.Entities;

namespace MedCore.Application.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task CreateDoctorAsync(string name, string phone, int specialtyId);
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(int specialtyId);
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task<OperationResult> UpdateDoctorAsync(int id, string name, string phone, int specialtyId);
        Task<OperationResult> DeleteDoctorAsync(int id);
        Task<int> GetCountAsync();
        Task<IEnumerable<Specialty>> GetSpecialtiesAsync();
    }
}
