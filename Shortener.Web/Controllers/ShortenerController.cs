using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Shortener.Core.Interfaces;

namespace Shortener.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public ShortenerController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody]JObject request)
        {
            var longUrl = request.GetValue("longUrl").ToString();

            if(!Uri.IsWellFormedUriString(longUrl, UriKind.Absolute))
            {
                return BadRequest();
            }

           var shortUrlPath = await _urlService.CreateShortUrl(longUrl);
            var shortUrl = $"{this.Request.Scheme}://{this.Request.Host}/{shortUrlPath}";
            return Ok(shortUrl);
        }

    }
}
