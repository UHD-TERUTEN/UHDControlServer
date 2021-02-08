using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UHDControlServer.Attributes;
using UHDControlServer.Models;

namespace UHDControlServer.Controllers
{
    [ApiController]
    [Route("api/file-access-reject-logs")]
    public class FileAccessRejectLogController : ControllerBase
    {
        public FileAccessRejectLogController(ILogger<FileAccessRejectLogController> logger, SqliteDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public async Task<IEnumerable<FileAccessRejectLog>> GetPage([FromQuery(Name = "page")] int page)
        {
            return await dbContext.FileAccessRejectLogs.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<FileAccessRejectLog> Get(int id)
        {
            return await dbContext.FileAccessRejectLogs
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}/inquiries/{inquiry-id:int}")]
        public async Task<Inquiry> GetInquiries(int id, int inquiryId)
        {
            var log = await dbContext.FileAccessRejectLogs
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();

            return log.Inquiries[0];
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, bool accept)
        {
            var log = await dbContext.FileAccessRejectLogs
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();

            log.IsAllowed = accept;
            dbContext.FileAccessRejectLogs.Update(log);
            return Ok(log);
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<FileAccessRejectLogController> logger;
    }
}
