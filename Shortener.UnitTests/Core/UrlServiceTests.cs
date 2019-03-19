using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shortener.Core.Services;
using Shortener.Data.Interfaces;
using Shortener.Data.Models;
using System.Threading.Tasks;

namespace Shortener.UnitTests
{
    [TestClass]
    public class UrlServiceTests
    {
        private Mock<IGenericRepository<UrlMap>> _mockRepository;
        private readonly UrlService _urlService;
        
        public UrlServiceTests()
        {
            _mockRepository = new Mock<IGenericRepository<UrlMap>>();
            _urlService = new UrlService(_mockRepository.Object);
        }

        [DataTestMethod]
        [DataRow(13335268, "37g8")]
        [DataRow(218857, "457")]
        [DataRow(14775679, "99Zz")]
        public async Task CreateShortUrl_ReturnsValidShortUrl(int input, string expected)
        {
            var testUrl = "https://www.google.com";
            _mockRepository.SetReturnsDefault<Task<UrlMap>>(Task.FromResult((new UrlMap(input, null, testUrl))));
            var actual = await _urlService.CreateShortUrl(testUrl);
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(13335268, "urlString1", "37g8")]
        [DataRow(218857, "urlString2", "457")]
        [DataRow(14775679, "urlString3", "99Zz")]
        public async Task GetLongUrl_ReturnsValidLongUrl(int id, string longUrl, string shortUrl)
        {
            _mockRepository.SetReturnsDefault<Task<UrlMap>>(Task.FromResult((new UrlMap(id, shortUrl, longUrl))));
            var actual = await _urlService.GetLongUrl(shortUrl);
            Assert.AreEqual(longUrl, actual);
        }

        [DataTestMethod]
        [DataRow("EMJG", 7298114)]
        [DataRow("gxJG5", 94276141)]
        [DataRow("5G44F", 850096415)]
        public async Task GetIDFromUrl_ReturnsCorrectValue(string input, int expected)
        {
            var actual = await _urlService.GetIDFromUrl(input);
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(13335268, "37g8")]
        [DataRow(218857, "457")]
        [DataRow(14775679, "99Zz")]
        public async Task GetUrlFromID_ReturnsCorrectValue(int input, string expected)
        {
            var actual = await _urlService.GetUrlFromID(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
