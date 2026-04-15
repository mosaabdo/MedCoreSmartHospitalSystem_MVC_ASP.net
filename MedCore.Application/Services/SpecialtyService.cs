using MedCore.Application.Common;
using MedCore.Domain.Entities;
using MedCore.Domain.Interfaces;

namespace MedCore.Application.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }
        public async Task<OperationResult> CreateSpecialtyAsync(string name)
        {
            bool exists = await _specialtyRepository.IsSpecialtyExistsAsync(name);
            if (exists)
            {
                return OperationResult.Failure($"The specialty '{name}' already exists in the system.");
            }
            var specialty = new Specialty { Name = name.Trim() };
            await _specialtyRepository.AddAsync(specialty);
            await _specialtyRepository.SaveChangesAsync();

            return OperationResult.Success("Specialty added successfully.");
        }
        public async Task<int> GetCountAsync() => await _specialtyRepository.GetCountAsync();

        //public async Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync() => await _specialtyRepository.GetAllAsync();
        public async Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync() => await _specialtyRepository.GetAllAsync();

        public async Task<Specialty?> GetSpecialtyByIdAsync(int id) => await _specialtyRepository.GetByIdAsync(id);

        //public async Task<OperationResult> AddSpecialtyAsync(string name)
        //{
        //    await _specialtyRepository.AddAsync(new Specialty { Name = name });
        //    await _specialtyRepository.SaveChangesAsync();
        //    return OperationResult.Success("Specialty added successfully.");
        //}

        public async Task<OperationResult> UpdateSpecialtyAsync(int id, string name)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty == null) return OperationResult.Failure("Specialty not found.");

            specialty.Name = name;
            _specialtyRepository.Update(specialty);
            await _specialtyRepository.SaveChangesAsync();

            return OperationResult.Success("Specialty updated successfully.");
        }

        public async Task<OperationResult> DeleteSpecialtyAsync(int id)
        {

            if (await _specialtyRepository.HasDoctorsAsync(id))
            {
                return OperationResult.Failure("Cannot delete this specialty because it has registered doctors.");
            }

            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty == null) return OperationResult.Failure("Specialty not found.");

            _specialtyRepository.Delete(specialty);
            await _specialtyRepository.SaveChangesAsync();

            return OperationResult.Success("Specialty deleted successfully.");
        }
    }
}
