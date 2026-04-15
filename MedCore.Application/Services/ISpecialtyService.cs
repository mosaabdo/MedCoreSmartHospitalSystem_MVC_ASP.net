using MedCore.Application.Common;
using MedCore.Domain.Entities;

namespace MedCore.Application.Services
{
    public interface ISpecialtyService
    {
        Task<OperationResult> CreateSpecialtyAsync(string name);
        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync();
        Task<int> GetCountAsync();
        Task<Specialty?> GetSpecialtyByIdAsync(int id);
        //Task<OperationResult> AddSpecialtyAsync(string name);
        Task<OperationResult> UpdateSpecialtyAsync(int id, string name);
        Task<OperationResult> DeleteSpecialtyAsync(int id);

    }
}
