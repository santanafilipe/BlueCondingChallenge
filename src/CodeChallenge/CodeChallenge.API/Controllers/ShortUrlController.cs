using CodeChallenge.API.Models;
using CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeChallenge.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlservice _shortUrlservice;
        private readonly ILogger<ShortUrlController> _logger;

        public ShortUrlController(IShortUrlservice shortUrlservice, ILogger<ShortUrlController> logger)
        {
            _shortUrlservice = shortUrlservice;
            _logger = logger;
        }

        // GET: api/<ShortUrlController>
        [HttpGet("{shortUrl}")]
        public ActionResult Get(string shortUrl)
        {
            var result = _shortUrlservice.GetUrl(shortUrl);
            return Redirect(result.Url);
        }

        [HttpGet("GetMostClicked",Name = "Most Clicked")]
        public List<ShortUrls> GetMostClicked()
        {
            var result = _shortUrlservice.GetMostClicked();
            return result;
        }

        // POST api/<ShortUrlController>
        [HttpPost(Name = "CreateUrl")]
        public ShortUrls Post([FromBody] string Url)
        {
            var result = _shortUrlservice.CreateShortUrl(Url);
            return result;
        }
    }
}
