using API.Data;
using API.Models.Entity;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API.Repository
{
    public class PropuestaRepository : IPropuestaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string PropuestaCacheKey = "PropuestaCacheKey";
        private readonly int CacheExpirationTime = 3600;
        public PropuestaRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() >= 0;
            if (result)
            {
                ClearCache();
            }
            return result;
        }

        public void ClearCache()
        {
            _cache.Remove(PropuestaCacheKey);
        }

        public async Task<ICollection<PropuestaEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(PropuestaCacheKey, out ICollection<PropuestaEntity> propuestasCached))
                return propuestasCached;

            var propuestasFromDb = await _context.Propuesta.OrderBy(c => c.Titulo).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(PropuestaCacheKey, propuestasFromDb, cacheEntryOptions);
            return propuestasFromDb;
        }

        public async Task<PropuestaEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(PropuestaCacheKey, out ICollection<PropuestaEntity> propuestasCached))
            {
                var propuesta = propuestasCached.FirstOrDefault(c => c.Id == id);
                if (propuesta != null)
                    return propuesta;
            }

            return await _context.Propuesta.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Propuesta.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(PropuestaEntity propuesta)
        {
            _context.Propuesta.Add(propuesta);
           
            return await Save();
        }

        public async Task<bool> UpdateAsync(PropuestaEntity propuesta)
        {
            
            _context.Update(propuesta);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var propuesta = await GetAsync(id);
            if (propuesta == null)
                return false;

            _context.Propuesta.Remove(propuesta);
            return await Save();
        }
    }
}
