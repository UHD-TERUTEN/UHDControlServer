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
    [Route("api/file-access-reject-logs")]
    public class FileAccessRejectLogController : ControllerBase
    {
        private readonly ILogger<FileAccessRejectLogController> _logger;

        public FileAccessRejectLogController(ILogger<FileAccessRejectLogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        public FileAccessRejectLog Get(int id)
        {
            return new FileAccessRejectLog()
            {
                Id = id,
                AgentId = 2,
                DateTime = DateTime.UtcNow,
                ProgramName = "program",
                Details = "details",
                IsAllowed = false,
                Inquiries = new Inquiry[] { },
            };
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public IEnumerable<FileAccessRejectLog> GetPage([FromQuery(Name = "page")] int page)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new FileAccessRejectLog
            {
                Id = index,
                AgentId = rng.Next(1, 10),
                DateTime = DateTime.UtcNow.AddDays(index),
                ProgramName = $"program-{index}",
                Details = $"details-{index}",
                IsAllowed = (rng.Next(1, 2) == 1),
                Inquiries = new Inquiry[] { },
            })
            .ToArray();
        }

        [HttpGet("{id:int}/inquiries/{inquiry-id:int}")]
        public Inquiry GetInquiries(int id, int inquiryId)
        {
            return new Inquiry()
            {
                Id = inquiryId,
                Title = "title",
                Log = "log",
                Details = "details",
            };
        }
    }
}
