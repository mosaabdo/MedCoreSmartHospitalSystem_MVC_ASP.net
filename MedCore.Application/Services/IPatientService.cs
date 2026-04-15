using MedCore.Application.Common;
using MedCore.Domain.Entities;

namespace MedCore.Application.Services
{
    public interface IPatientService
    {
        Task<OperationResult> RegisterPatientAsync(string name,
            string phone);
        Task<OperationResult> RegisterPatientAsync(int userId, string name, string phone);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<OperationResult> UpdatePatientAsync(int id, string name, string phone);
        Task<OperationResult> DeletePatientAsync(int id);
        Task<int> GetCountAsync();
    }
}
