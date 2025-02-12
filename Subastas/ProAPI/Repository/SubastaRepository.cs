using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Repository
{
    public class SubastaRepository : ISubastaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string SubastaEntityCacheKey = "SubastaEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        public SubastaRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(SubastaEntityCacheKey);
        }

        public async Task<ICollection<SubastasEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(SubastaEntityCacheKey, out ICollection<SubastasEntity> SubastaCached))
                return SubastaCached;

            var SubastaFromDb = await _context.Subastas.OrderBy(c => c.Name).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(SubastaEntityCacheKey, SubastaFromDb, cacheEntryOptions);
            return SubastaFromDb;
        }

        public async Task<SubastasEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(SubastaEntityCacheKey, out ICollection<SubastasEntity> SovietTanksCached))
            {
                var SubastaEntity = SovietTanksCached.FirstOrDefault(c => c.Id == id);
                if (SubastaEntity != null)
                    return SubastaEntity;
            }

            return await _context.Subastas.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Subastas.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(SubastasEntity SubastaEntity)
        {
            _context.Subastas.Add(SubastaEntity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(SubastasEntity SubastaEntity)
        {
            SubastaEntity.CreatedDate = DateTime.Now;
            _context.Update(SubastaEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var SubastaEntity = await GetAsync(id);
            if (SubastaEntity == null)
                return false;

            _context.Subastas.Remove(SubastaEntity);
            return await Save();
        }


    }
}
