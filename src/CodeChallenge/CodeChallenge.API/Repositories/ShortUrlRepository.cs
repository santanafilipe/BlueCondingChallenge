using CodeChallenge.API.Data;
using CodeChallenge.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.API.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly CodeChallengeContext _context;

        public ShortUrlRepository(CodeChallengeContext context)
        {
            _context = context;
        }

        public void Add(ShortUrls entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(ShortUrls entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<ShortUrls> GetAll()
        {
            return _context.Set<ShortUrls>();
        }
    }
}
