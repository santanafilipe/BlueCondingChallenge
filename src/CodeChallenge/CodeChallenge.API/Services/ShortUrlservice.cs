using CodeChallenge.API.Models;
using CodeChallenge.API.Repositories;
using System.Web;

namespace CodeChallenge.API.Services
{
    public class ShortUrlservice : IShortUrlservice
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IConfiguration _configuration;

        public ShortUrlservice(IShortUrlRepository shortUrlRepository, IConfiguration configuration)
        {
            _shortUrlRepository = shortUrlRepository;
            _configuration = configuration;
        }

        public ShortUrls CreateShortUrl(string url)
        {
            var baseUrl = _configuration.GetValue<string>("AppSettings:BaseUrl");
            var shortUrl = ValidateLongUrlExist(url);

            if (shortUrl is not null)
                return shortUrl;

            var path = GeneratePath();
            shortUrl = new ShortUrls()
            {
                Url = url,
                ShortUrl = $"{baseUrl}/{path}",
                Clicked = 0
            };

            _shortUrlRepository.Add(shortUrl);

            return shortUrl;
        }

        private string GeneratePath()
        {
            string path = string.Empty;
            Enumerable.Range(48, 75)
                .Where(x => x < 58 || x > 64 && x < 91 || x > 96)
                .OrderBy(o => new Random().Next())
                .ToList()
                .ForEach(i => path += Convert.ToChar(i));

            int startIndex = new Random().Next(0, path.Length);
            int length = new Random().Next(2, 6);
            int endIndex = startIndex + length;

            string resultPath = string.Empty;
            for (int i = startIndex; i < endIndex; i++)
                resultPath += path[i % path.Length];

            return resultPath;
        }

        private ShortUrls ValidateLongUrlExist(string url)
        {
            return _shortUrlRepository.GetAll().FirstOrDefault(s => s.Url.ToLower().Equals(url.ToLower()));
        }

        public ShortUrls GetUrl(string ShortUrl)
        {
            ShortUrl = HttpUtility.UrlDecode(ShortUrl);
            var url = _shortUrlRepository.GetAll().FirstOrDefault(s => s.ShortUrl.ToLower().Equals(ShortUrl.ToLower()));

            if (url is not null)
            {
                url.Clicked++;
                _shortUrlRepository.Update(url);
            }

            return url;
        }

        public List<ShortUrls> GetMostClicked()
        {
            return _shortUrlRepository.GetAll()
                .OrderByDescending(s => s.Clicked)
                .Take(100)
                .ToList();
        }
    }
}
