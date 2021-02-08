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
    [Route("api/system-logs")]
    public class SystemLogController : ControllerBase
    {
        private readonly ILogger<SystemLogController> _logger;

        public SystemLogController(ILogger<SystemLogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        public SystemLog Get(int id)
        {
            return new SystemLog()
            {
                Id = id,
                AgentId = 2,
                DateTime = DateTime.UtcNow,
                Size = 1024,
            };
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public IEnumerable<SystemLog> GetPage([FromQuery(Name = "page")] int page)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new SystemLog
            {
                Id = index,
                AgentId = rng.Next(1, 10),
                DateTime = DateTime.UtcNow.AddDays(index),
                Size = 1024 * index,
            })
            .ToArray();
        }
    }
}
