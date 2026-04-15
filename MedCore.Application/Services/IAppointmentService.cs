using MedCore.Application.Common;
using MedCore.Domain.Entities;

namespace MedCore.Application.Services
{
    public interface IAppointmentService
    {
        Task<OperationResult> BookAppointmentAsync(int patientId,
            int doctorId, DateTime appointmentDate, string notes);

        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<OperationResult> UpdateAppointmentAsync(int id, int patientId, int doctorId, DateTime appointmentDate, string? notes);
        Task<int> GetTodayAppointmentsCountAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<OperationResult> CancelAppointmentAsync(int id);
        Task<int> GetCountAsync();
        Task<List<Doctor>> GetDoctorsAsync();
        Task<List<Patient>> GetPatientsAsync();
    }
}
