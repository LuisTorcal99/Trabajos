using API.Data;
using API.Models.Entity;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API.Repository
{
    public class CursoRepository : ICursoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string CursoCacheKey = "CursoCacheKey";
        private readonly int CacheExpirationTime = 3600;
        public CursoRepository(ApplicationDbContext context, IMemoryCache cache)
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
            _cache.Remove(CursoCacheKey);
        }

        public async Task<ICollection<CursoEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(CursoCacheKey, out ICollection<CursoEntity> cursosCached))
                return cursosCached;

            var cursosFromDb = await _context.Curso.OrderBy(c => c.Nombre).ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheExpirationTime));

            _cache.Set(CursoCacheKey, cursosFromDb, cacheEntryOptions);
            return cursosFromDb;
        }

        public async Task<CursoEntity> GetAsync(int id)
        {
            if (_cache.TryGetValue(CursoCacheKey, out ICollection<CursoEntity> cursosCached))
            {
                var curso = cursosCached.FirstOrDefault(c => c.Id == id);
                if (curso != null)
                    return curso;
            }

            return await _context.Curso.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Curso.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateAsync(CursoEntity curso)
        {
            _context.Curso.Add(curso);
            return await Save();
        }

        public async Task<bool> UpdateAsync(CursoEntity curso)
        {

            _context.Update(curso);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var curso = await GetAsync(id);
            if (curso == null)
                return false;

            _context.Curso.Remove(curso);
            return await Save();
        }
    }
}
