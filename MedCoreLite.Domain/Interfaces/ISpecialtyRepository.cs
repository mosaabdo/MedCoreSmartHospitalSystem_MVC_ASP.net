using MedCore.Domain.Entities;

namespace MedCore.Domain.Interfaces
{
    public interface ISpecialtyRepository : IGenericRepository<Specialty>
    {
        //Task AddAsync(Specialty specialty);
        Task<bool> IsSpecialtyExistsAsync(string name);
        Task<bool> HasDoctorsAsync(int specialtyId);
        //Task<IEnumerable<Specialty>> GetAllAsync();
        //Task<int> GetCountAsync();
        //Task SaveChangesAsync();
    }
}
