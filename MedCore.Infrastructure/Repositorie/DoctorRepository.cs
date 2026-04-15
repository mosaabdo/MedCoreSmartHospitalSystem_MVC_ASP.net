using MedCore.Domain.Entities;
using MedCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Infrastructure.Repositorie
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        //private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {

        }

        //public async Task<int> GetCountAsync() => await _context.Doctors.CountAsync();
        //public async Task<IEnumerable<Doctor>> GetAllAsync()
        //{
        //    return await _context.Doctors.ToListAsync();

        //}

        public async Task<Doctor?> GetDoctorDetailsAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<bool> HasAppointmentsAsync(int doctorId)
        {

            return await _context.Appointments.AnyAsync(a => a.DoctorId == doctorId);
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(int specialtyId)
        {
            return await _context.Doctors
                .Where(d => d.SpecialtyId == specialtyId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Specialty>> GetSpecialtiesAsync()
        {
            return await _context.Specialties.ToListAsync();
        }

        //public async Task AddAsync(Doctor doctor)
        //{
        //    await _context.Doctors.AddAsync(doctor);
        //}
        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }
}
