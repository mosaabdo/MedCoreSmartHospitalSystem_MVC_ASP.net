using MedCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Infrastructure.Repositorie
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        //public DbSet<T> sset{ get; set; }
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public async Task<int> GetCountAsync() => await _context.Set<T>().CountAsync();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
