using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Core.Interfaces
{
    public interface IUrlService
    {
        Task<string> CreateShortUrl(string longUrl);
        Task<string> GetLongUrl(string shortUrl);
        Task<bool> IsValidShortUrl(string shortUrl);
    }
}
