using MedCore.Application.Common;
using MedCore.Domain.Entities;
using MedCore.Domain.Interfaces;

namespace MedCore.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(int specialtyId)
        {
            return await _doctorRepository.GetDoctorsBySpecialtyAsync(specialtyId);
        }
        public async Task<int> GetCountAsync() => await _doctorRepository.GetCountAsync();
        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.GetByIdAsync(id);
        }

        public async Task<OperationResult> UpdateDoctorAsync(int id, string name, string phone, int specialtyId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null) return OperationResult.Failure("Doctor not found.");


            doctor.Name = name;
            doctor.Phone = phone;
            doctor.SpecialtyId = specialtyId;

            _doctorRepository.Update(doctor);
            await _doctorRepository.SaveChangesAsync();

            return OperationResult.Success("Doctor updated successfully.");
        }

        public async Task<OperationResult> DeleteDoctorAsync(int id)
        {

            bool hasAppointments = await _doctorRepository.HasAppointmentsAsync(id);
            if (hasAppointments)
            {
                return OperationResult.Failure("Cannot delete this doctor because they have existing appointments.");
            }

            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null) return OperationResult.Failure("Doctor not found.");

            _doctorRepository.Delete(doctor);
            await _doctorRepository.SaveChangesAsync();

            return OperationResult.Success("Doctor deleted successfully.");
        }

        public async Task CreateDoctorAsync(string name, string phone, int specialtyId)
        {
            var doctor = new Doctor
            {
                Name = name,
                Phone = phone,
                SpecialtyId = specialtyId
            };

            await _doctorRepository.AddAsync(doctor);
            await _doctorRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Specialty>> GetSpecialtiesAsync()
        {
            return await _doctorRepository.GetSpecialtiesAsync();
        }
    }
}
