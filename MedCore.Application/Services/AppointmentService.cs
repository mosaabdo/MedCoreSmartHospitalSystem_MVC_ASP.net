using MedCore.Application.Common;
using MedCore.Domain.Entities;
using MedCore.Domain.Interfaces;

namespace MedCore.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetAppointmentWithDetailsAsync(id);
        }
        public async Task<OperationResult> UpdateAppointmentAsync(int id, int patientId, int doctorId, DateTime appointmentDate, string? notes)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return OperationResult.Failure("Appointment not found.");

            if (appointment.AppointmentDate != appointmentDate || appointment.DoctorId != doctorId)
            {
                bool isAvailable = await _appointmentRepository.IsTimeSlotAvailableAsync(doctorId, appointmentDate);
                if (!isAvailable) return OperationResult.Failure("The doctor is already booked at this new time.");
            }

            appointment.PatientId = patientId;
            appointment.DoctorId = doctorId;
            appointment.AppointmentDate = appointmentDate;
            appointment.Notes = notes;

            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return OperationResult.Success("Appointment updated successfully.");
        }
        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _appointmentRepository.GetDoctorsAsync();
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _appointmentRepository.GetPatientsAsync();
        }

        public async Task<int> GetCountAsync() => await _appointmentRepository.GetCountAsync();
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }
        public async Task<int> GetTodayAppointmentsCountAsync()
        {
            return await _appointmentRepository.GetTodayAppointmentsCountAsync();
        }
        public async Task<OperationResult> BookAppointmentAsync(int patientId,
            int doctorId, DateTime appointmentDate, string notes)
        {
            if (appointmentDate < DateTime.Now)
            {
                return OperationResult.Failure("Cannot book an appointment in the past.");
            }

            bool isBooked = await _appointmentRepository
                .IsAppointmentExistsAsync(doctorId, appointmentDate);

            if (isBooked)
            {
                return OperationResult.Failure("The doctor is already booked at this time. Please choose another slot.");
            }


            var newAppointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                Notes = notes
            };

            await _appointmentRepository.AddAsync(newAppointment);
            await _appointmentRepository.SaveChangesAsync();

            return OperationResult.Success("Success, Your Booked");
        }
        public async Task<OperationResult> CancelAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return OperationResult.Failure("Appointment not found.");

            _appointmentRepository.Delete(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return OperationResult.Success("Appointment cancelled successfully.");
        }
    }

}
