using eThorWebApp.Shared.Data;
using eThorWebApp.Shared.Interfaces;
using eThorWebApp.Shared.Models;
using System.Linq;
using System.Threading.Tasks;

namespace eThorWebApp.Shared.Services
{
    public class ethorService : IethorService
    {
        private readonly eThorTestEntityContext _context;

        public ethorService(eThorTestEntityContext context)
        {
            _context = context;
        }

        public async Task Add(eThorTestEntity entity)
        {
            _context.eThorTestEntites.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            eThorTestEntity entity = _context.Set<eThorTestEntity>().SingleOrDefault(e => e.Id == Id);
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.Set<eThorTestEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<eThorTestEntity> Get(int Id)
        {
            return await Task.FromResult(_context.Set<eThorTestEntity>().SingleOrDefault(e => e.Id == Id));
        }

        public Task<eThorTestEntity[]> GetAll()
        {
            return Task.FromResult(_context.eThorTestEntites.ToArray());
        }

        public async Task Update(eThorTestEntity entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
