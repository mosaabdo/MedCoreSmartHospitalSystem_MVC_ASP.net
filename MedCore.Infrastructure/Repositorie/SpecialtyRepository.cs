using MedCore.Domain.Entities;
using MedCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Infrastructure.Repositorie
{
    public class SpecialtyRepository : GenericRepository<Specialty>, ISpecialtyRepository
    {
        //private readonly ApplicationDbContext _context;
        public SpecialtyRepository(ApplicationDbContext context) : base(context) { } //=> _context = context;

        //public async Task AddAsync(Specialty specialty) => await _context.Specialties.AddAsync(specialty);
        //public async Task<IEnumerable<Specialty>> GetAllAsync() => await _context.Specialties.ToListAsync();
        //public async Task<int> GetCountAsync() => await _context.Specialties.CountAsync();

        public async Task<bool> HasDoctorsAsync(int specialtyId)
        {
            return await _context.Doctors.AnyAsync(d => d.SpecialtyId == specialtyId);
        }
        public async Task<bool> IsSpecialtyExistsAsync(string name)
        {
            return await _context.Specialties.AnyAsync(s => s.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<Specialty?> GetSpecialtyByIdAsync(int id)
        {
            return await _context.Specialties
                .Include(s => s.Doctors)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        //public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
