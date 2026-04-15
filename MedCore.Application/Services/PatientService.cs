using MedCore.Application.Common;
using MedCore.Domain.Entities;
using MedCore.Domain.Interfaces;

namespace MedCore.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<OperationResult> RegisterPatientAsync(int userId, string name, string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                bool exists = await _patientRepository.IsPhoneExistsAsync(phone);
                if (exists)
                {
                    return OperationResult.Failure($"A patient with phone number '{phone}' is already registered.");
                }
            }

            var patient = new Patient
            {
                Id = userId,
                Name = name,
                Phone = phone ?? string.Empty
            };

            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveChangesAsync();

            return OperationResult.Success("Patient registered successfully.");
        }
        public async Task<int> GetCountAsync() => await _patientRepository.GetCountAsync();
        public async Task<OperationResult> RegisterPatientAsync(string name, string phone)
        {

            bool exists = await _patientRepository.IsPhoneExistsAsync(phone);
            if (exists)
            {
                return OperationResult.Failure($"A patient with phone number '{phone}' is already registered.");
            }

            var patient = new Patient
            {
                Name = name,
                Phone = phone
            };

            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveChangesAsync();

            return OperationResult.Success("Patient registered successfully.");
        }
        public async Task<Patient?> GetPatientByIdAsync(int id)
            => await _patientRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
            => await _patientRepository.GetAllAsync();

        public async Task<OperationResult> UpdatePatientAsync(int id, string name, string phone)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) return OperationResult.Failure("Patient not found.");

            if (patient.Phone != phone && await _patientRepository.IsPhoneExistsAsync(phone))
                return OperationResult.Failure("This phone number is already registered to another patient.");

            patient.Name = name;
            patient.Phone = phone;

            _patientRepository.Update(patient);
            await _patientRepository.SaveChangesAsync();

            return OperationResult.Success("Patient updated successfully.");
        }

        public async Task<OperationResult> DeletePatientAsync(int id)
        {

            bool hasAppointments = await _patientRepository.HasAppointmentsAsync(id);
            if (hasAppointments)
            {
                return OperationResult.Failure("Cannot delete this patient because they have existing appointments.");
            }

            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) return OperationResult.Failure("Patient not found.");

            _patientRepository.Delete(patient);
            await _patientRepository.SaveChangesAsync();

            return OperationResult.Success("Patient deleted successfully.");
        }
    }
}
