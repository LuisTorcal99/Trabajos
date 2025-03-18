using API.Data;
using API.Models.Entity;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API.Repository
{
    public class UserPracticoRepository : IUserPracticoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string UsersCacheKey = "UsersCacheKey";
        private readonly int CacheExpirationTime = 3600;
        public UserPracticoRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(UsersCacheKey);
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            if (_cache.TryGetValue(UsersCacheKey, out ICollection<User> propuestasCached))
                return propuestasCached;

            var propuestasFromDb = await _context.Users.OrderBy(c => c.Name).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(UsersCacheKey, propuestasFromDb, cacheEntryOptions);
            return propuestasFromDb;
        }

        public async Task<User> GetAsync(int id)
        {
            if (_cache.TryGetValue(UsersCacheKey, out ICollection<User> propuestasCached))
            {
                var propuesta = propuestasCached.FirstOrDefault(c => c.Id == id);
                if (propuesta != null)
                    return propuesta;
            }

            return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(User propuesta)
        {
            _context.Users.Add(propuesta);

            return await Save();
        }

        public async Task<bool> UpdateAsync(User propuesta)
        {

            _context.Update(propuesta);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var propuesta = await GetAsync(id);
            if (propuesta == null)
                return false;

            _context.Users.Remove(propuesta);
            return await Save();
        }

       
    }
}
