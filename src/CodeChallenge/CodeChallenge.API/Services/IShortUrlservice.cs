using CodeChallenge.API.Models;

namespace CodeChallenge.API.Services
{
    public interface IShortUrlservice
    {
        ShortUrls CreateShortUrl(string url);
        ShortUrls GetUrl(string url);

        List<ShortUrls> GetMostClicked();
    }
}