using ExamenFinalApi.Data;
using ExamenFinalApi.Models.Entity;
using ExamenFinalApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ExamenFinalApi.Repository
{
    public class ObjetoTresRepository : IObjetoTresRepository
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string ObjetoTresEntityCacheKey = "ObjetoTresEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        public ObjetoTresRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(ObjetoTresEntityCacheKey);
        }

        public async Task<ICollection<ObjetoTresEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(ObjetoTresEntityCacheKey, out ICollection<ObjetoTresEntity> ObjetoTressCached))
                return ObjetoTressCached;

            var ObjetoTressFromDb = await _context.ObjetoTres.OrderBy(c => c.Id).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(ObjetoTresEntityCacheKey, ObjetoTressFromDb, cacheEntryOptions);
            return ObjetoTressFromDb;
        }

        public async Task<ObjetoTresEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(ObjetoTresEntityCacheKey, out ICollection<ObjetoTresEntity> ObjetoTressCached))
            {
                var ObjetoTresEntity = ObjetoTressCached.FirstOrDefault(c => c.Id == id);
                if (ObjetoTresEntity != null)
                    return ObjetoTresEntity;
            }

            return await _context.ObjetoTres.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ObjetoTres.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(ObjetoTresEntity ObjetoTresEntity)
        {
            ObjetoTresEntity.Fecha = DateTime.Now;
            _context.ObjetoTres.Add(ObjetoTresEntity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(ObjetoTresEntity ObjetoTresEntity)
        {
            _context.Update(ObjetoTresEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ObjetoTresEntity = await GetAsync(id);
            if (ObjetoTresEntity == null)
                return false;

            _context.ObjetoTres.Remove(ObjetoTresEntity);
            return await Save();
        }
    }
}

