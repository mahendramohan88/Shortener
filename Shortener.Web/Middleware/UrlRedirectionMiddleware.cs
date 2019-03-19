using Microsoft.AspNetCore.Http;
using Shortener.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortener.Web.Middleware
{
    public class UrlRedirectionMiddleware
    {
        private readonly RequestDelegate _next;

        public UrlRedirectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUrlService urlService)
        {
            var path = context.Request.Path.ToUriComponent();

            if (await PathIsShortUrl(path, urlService))
            {
                var longUrl = await urlService.GetLongUrl(path.Trim('/'));
                if (longUrl != null)
                {
                    context.Response.Redirect(longUrl);
                    return;
                }
            }

            await _next.Invoke(context);
        }

        private async Task<bool> PathIsShortUrl(string path, IUrlService urlService)
        {
            if (path == String.Empty)
            {
                return false;
            }

            if (path.StartsWith("/api/"))
            {
                return false;
            }

            if (await urlService.IsValidShortUrl(path.Trim('/')))
            {
                return true;
            }

            return false;
        }

    }
}
