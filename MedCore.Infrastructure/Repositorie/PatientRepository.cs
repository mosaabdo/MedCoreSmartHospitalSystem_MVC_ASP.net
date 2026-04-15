using MedCore.Domain.Entities;
using MedCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Infrastructure.Repositorie
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        //private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<bool> HasAppointmentsAsync(int patientId)
        {
            return await _context.Appointments.AnyAsync(a => a.PatientId == patientId);
        }
        //public async Task AddAsync(Patient patient) => await _context.Patients.AddAsync(patient);

        //public async Task<int> GetCountAsync() => await _context.Patients.CountAsync();
        public async Task<bool> IsPhoneExistsAsync(string phone) =>
            await _context.Patients.AnyAsync(p => p.Phone == phone);

        //public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
