using MedCore.Domain.Entities;
using MedCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Infrastructure.Repositorie
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        //private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<bool> IsAppointmentExistsAsync(int doctorId,
            DateTime appointmentDate)
        {
            return await _context.Appointments
                .AnyAsync(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDate);
        }
        public async Task<Appointment?> GetAppointmentWithDetailsAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }
        //public async Task<int> GetCountAsync() => await _context.Appointments.CountAsync();
        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }
        public async Task<int> GetTodayAppointmentsCountAsync()
        {
            var today = DateTime.Today;
            return await _context.Appointments
                .CountAsync(a => a.AppointmentDate.Date == today);
        }
        //public async Task AddAsync(Appointment appointment)
        //{
        //    await _context.Appointments.AddAsync(appointment);
        //}

        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
        public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<bool> IsTimeSlotAvailableAsync(int doctorId, DateTime appointmentDate)
        {
            bool isBooked = await _context.Appointments
                .AnyAsync(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDate);

            return !isBooked;
        }
    }
}
