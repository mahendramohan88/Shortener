using Microsoft.AspNetCore.Http;
using Shortener.Core.Interfaces;
using Shortener.Data.Interfaces;
using Shortener.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Core.Services
{
    public class UrlService : IUrlService
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static int Base = Alphabet.Length;

        private readonly IGenericRepository<UrlMap> _repository;

        public UrlService(IGenericRepository<UrlMap> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateShortUrl(string longUrl)
        {
            var urlMap = await _repository.Create(new UrlMap(longUrl));
            urlMap.ShortUrl = await GetUrlFromID(urlMap.ID);
            await _repository.Update(urlMap);
            return urlMap.ShortUrl;
        }

        public async Task<string> GetLongUrl(string shortUrl)
        {
            int id = await GetIDFromUrl(shortUrl);
            var urlMap = await _repository.Get(id);
            if(urlMap != null)
            {
                return urlMap.LongUrl;
            }
            return "Error";
        }

        public async Task<bool> IsValidShortUrl(string shortUrl)
        {
            int id = await GetIDFromUrl(shortUrl);
            return await _repository.Exists(id);
        }

        public async Task<string> GetUrlFromID(int input)
        {
            var stringBuilder = new StringBuilder();
            while (input > 0)
            {
                stringBuilder.Insert(0, Alphabet.ElementAt(input % Base));
                input = input / Base;
            }
            return stringBuilder.ToString();
        }

        public async Task<int> GetIDFromUrl(string input)
        {
            var number = 0;
            for(var i = 0; i < input.Length; i++)
            {
                number = number * Base + Alphabet.IndexOf(input.ElementAt(i));
            }
            return number;
        }
    }
}
