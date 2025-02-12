using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Repository
{
    public class HouseUserRepository : IHouseUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string HouseUserEntityCacheKey = "HouseUserEntityCacheKey"; //cambiadmelo lokos
        private readonly int CacheExpirationTime = 3600;
        public HouseUserRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(HouseUserEntityCacheKey);
        }

        public async Task<ICollection<HouseUserEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(HouseUserEntityCacheKey, out ICollection<HouseUserEntity> HouseUserCached))
                return HouseUserCached;

            var HouseUserFromDb = await _context.HouseUser.OrderBy(c => c.Email).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(HouseUserEntityCacheKey, HouseUserFromDb, cacheEntryOptions);
            return HouseUserFromDb;
        }

        public async Task<HouseUserEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(HouseUserEntityCacheKey, out ICollection<HouseUserEntity> SovietTanksCached))
            {
                var HouseUserEntity = SovietTanksCached.FirstOrDefault(c => c.Id == id);
                if (HouseUserEntity != null)
                    return HouseUserEntity;
            }

            return await _context.HouseUser.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.HouseUser.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(HouseUserEntity HouseUserEntity)
        {
            _context.HouseUser.Add(HouseUserEntity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(HouseUserEntity HouseUserEntity)
        {
            HouseUserEntity.CreatedDate = DateTime.Now;
            _context.Update(HouseUserEntity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var HouseUserEntity = await GetAsync(id);
            if (HouseUserEntity == null)
                return false;

            _context.HouseUser.Remove(HouseUserEntity);
            return await Save();
        }


    }
}
