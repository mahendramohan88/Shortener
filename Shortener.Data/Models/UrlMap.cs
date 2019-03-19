using System;
using System.Collections.Generic;
using System.Text;

namespace Shortener.Data.Models
{
    public class UrlMap
    {
        public int ID { get; set; }
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }

        public UrlMap(string longUrl)
        {
            LongUrl = longUrl ?? throw new ArgumentNullException(nameof(longUrl));
        }

        public UrlMap(int id, string shortUrl, string longUrl)
        {
            ID = id;
            ShortUrl = shortUrl;
            LongUrl = longUrl;
        }
    }
}
