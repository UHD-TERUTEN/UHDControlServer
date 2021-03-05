using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        [HttpGet("{id:int}/inquiries/{inquiryId:int}")]
        public async Task<Inquiry> GetInquiries(int id, int inquiryId)
        {
            var log = await dbContext.FileAccessRejectLogs
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();

            return (log.Inquiries.Length > inquiryId - 1)
                ? log.Inquiries[inquiryId - 1]
                : new Inquiry() { Id = 1, Title = "empty", Log = "empty", Details = "empty" };
        }

        [HttpPut]
        public async Task<IActionResult> Put(FileAccessRejectLog log)
        {
            dbContext.FileAccessRejectLogs.Update(log);
            await dbContext.SaveChangesAsync();
            return Ok(log);
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<FileAccessRejectLogController> logger;
    }
}
