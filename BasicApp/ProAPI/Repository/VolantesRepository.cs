using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Repository
{
    public class VolantesRepository : IVolantesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string VolantesEntityCacheKey = "VolantesEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        public VolantesRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(VolantesEntityCacheKey);
        }

        public async Task<ICollection<VolantesEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(VolantesEntityCacheKey, out ICollection<VolantesEntity> VolantesCached))
                return VolantesCached;

            var volantesFromDb = await _context.Volantes.OrderBy(c => c.Base).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(VolantesEntityCacheKey, volantesFromDb, cacheEntryOptions);
            return volantesFromDb;
        }

        public async Task<VolantesEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(VolantesEntityCacheKey, out ICollection<VolantesEntity> SovietTanksCached))
            {
                var VolantesEntity = SovietTanksCached.FirstOrDefault(c => c.Id == id);
                if (VolantesEntity != null)
                    return VolantesEntity;
            }

            return await _context.Volantes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Volantes.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(VolantesEntity VolantesEntity)
        {
            _context.Volantes.Add(VolantesEntity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(VolantesEntity VolantesEntity)
        {
            VolantesEntity.FechaSalida = DateTime.Now;
            _context.Update(VolantesEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var VolantesEntity = await GetAsync(id);
            if (VolantesEntity == null)
                return false;

            _context.Volantes.Remove(VolantesEntity);
            return await Save();
        }


    }
}
