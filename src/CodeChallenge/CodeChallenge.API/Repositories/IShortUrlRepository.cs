using CodeChallenge.API.Models;

namespace CodeChallenge.API.Repositories
{
    public interface IShortUrlRepository
    {
        void Add(ShortUrls entity);
        IEnumerable<ShortUrls> GetAll();
        void Update(ShortUrls entity);
    }
}