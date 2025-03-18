using ExamenFinalApi.Data;
using ExamenFinalApi.Models.Entity;
using ExamenFinalApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ExamenFinalApi.Repository
{
    public class ObjetoDosRepository : IObjetoDosRepository
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string ObjetoDosEntityCacheKey = "ObjetoDosEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        public ObjetoDosRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(ObjetoDosEntityCacheKey);
        }

        public async Task<ICollection<ObjetoDosEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(ObjetoDosEntityCacheKey, out ICollection<ObjetoDosEntity> ObjetoDossCached))
                return ObjetoDossCached;

            var ObjetoDossFromDb = await _context.ObjetoDos.OrderBy(c => c.Nombre).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(ObjetoDosEntityCacheKey, ObjetoDossFromDb, cacheEntryOptions);
            return ObjetoDossFromDb;
        }

        public async Task<ObjetoDosEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(ObjetoDosEntityCacheKey, out ICollection<ObjetoDosEntity> ObjetoDossCached))
            {
                var ObjetoDosEntity = ObjetoDossCached.FirstOrDefault(c => c.Id == id);
                if (ObjetoDosEntity != null)
                    return ObjetoDosEntity;
            }

            return await _context.ObjetoDos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ObjetoDos.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(ObjetoDosEntity ObjetoDosEntity)
        {
            _context.ObjetoDos.Add(ObjetoDosEntity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(ObjetoDosEntity ObjetoDosEntity)
        {
            _context.Update(ObjetoDosEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ObjetoDosEntity = await GetAsync(id);
            if (ObjetoDosEntity == null)
                return false;

            _context.ObjetoDos.Remove(ObjetoDosEntity);
            return await Save();
        }
    }
}

