using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UHDControlServer.Attributes;
using UHDControlServer.Models;

namespace UHDControlServer.Controllers
{
    [ApiController]
    [Route("api/whitelist")]
    public class WhitelistController : ControllerBase
    {
        private readonly ILogger<WhitelistController> _logger;

        public WhitelistController(ILogger<WhitelistController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        public Whitelist Get(int id)
        {
            return new Whitelist()
            {
                Id = id,
                Version = "1.0.0",
                LastUpdated = DateTime.UtcNow.AddDays(-1),
                LastDistributed = DateTime.UtcNow,
            };
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public IEnumerable<Whitelist> GetPage([FromQuery(Name = "page")] int page)
        {
            var rng = new Random();
            return Enumerable.Range(0, 4).Select(index => new Whitelist
            {
                Id = index,
                Version = $"1.0.{index}",
                LastUpdated = DateTime.UtcNow.AddDays(index -4),
                LastDistributed = DateTime.UtcNow,
            })
            .ToArray();
        }

        [HttpGet]
        [ExactQueryParam("version")]
        public IEnumerable<Whitelist> GetByVersion([FromQuery(Name = "version")] string version)
        {
            var rng = new Random();
            return Enumerable.Range(0, 4)
                .Select(index => new Whitelist
                {
                    Id = index,
                    Version = $"1.0.{index}",
                    LastUpdated = DateTime.UtcNow.AddDays(index - 4),
                    LastDistributed = DateTime.UtcNow,
                })
                .Where(whitelist => whitelist.Version == version)
                .ToArray();
        }
    }
}
