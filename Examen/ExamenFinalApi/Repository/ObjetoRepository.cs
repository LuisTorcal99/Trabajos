using ExamenFinalApi.Data;
using ExamenFinalApi.Models.Entity;
using ExamenFinalApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ExamenFinalApi.Repository
{
    public class ObjetoRepository : IObjetoRepository
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string ObjetoEntityCacheKey = "ObjetoEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        public ObjetoRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(ObjetoEntityCacheKey);
        }

        public async Task<ICollection<ObjetoEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(ObjetoEntityCacheKey, out ICollection<ObjetoEntity> ObjetosCached))
                return ObjetosCached;

            var ObjetosFromDb = await _context.Objeto.OrderBy(c => c.Nombre).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(ObjetoEntityCacheKey, ObjetosFromDb, cacheEntryOptions);
            return ObjetosFromDb;
        }

        public async Task<ObjetoEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(ObjetoEntityCacheKey, out ICollection<ObjetoEntity> ObjetosCached))
            {
                var ObjetoEntity = ObjetosCached.FirstOrDefault(c => c.Id == id);
                if (ObjetoEntity != null)
                    return ObjetoEntity;
            }

            return await _context.Objeto.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Objeto.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(ObjetoEntity ObjetoEntity)
        {
            _context.Objeto.Add(ObjetoEntity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(ObjetoEntity ObjetoEntity)
        {
            _context.Update(ObjetoEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ObjetoEntity = await GetAsync(id);
            if (ObjetoEntity == null)
                return false;

            _context.Objeto.Remove(ObjetoEntity);
            return await Save();
        }
    }
}

