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
    [Route("api/file-access-reject-log")]
    public class FileAccessRejectLogController : ControllerBase
    {
        public FileAccessRejectLogController(ILogger<FileAccessRejectLogController> logger, SqliteDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public async Task<IActionResult> GetPage([FromQuery(Name = "page")] int page)
        {
            if (page < 1)
                return BadRequest($"Out of range: {page}");

            var fileAccessRejectLogs = await dbContext.FileAccessRejectLogs.ToListAsync();
            return Ok(fileAccessRejectLogs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest($"Out of range: {id}");

            var fileAccessRejectLog =  await dbContext.FileAccessRejectLogs
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();
            return Ok(fileAccessRejectLog);
        }

        [HttpGet("{id:int}/inquiries/{inquiryId:int}")]
        public async Task<IActionResult> GetInquiries(int id, int inquiryId)
        {
            if (id < 1)         return BadRequest($"Out of range: {id}");
            if (inquiryId < 1)  return BadRequest($"Out of range: {inquiryId}");

            var log = await dbContext.FileAccessRejectLogs
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();
            return Ok(log);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] FileAccessRejectLog fileAccessRejectLog)
        {
            var fileAccessRejectLogEntry = dbContext.FileAccessRejectLogs.Update(fileAccessRejectLog);
            var changedEntry = fileAccessRejectLogEntry.Context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            if (changedEntry.Count != 1)
            {
                fileAccessRejectLogEntry.Reload();
                return BadRequest($"Invalid data: {fileAccessRejectLog}");
            }
            else
            {
                await dbContext.SaveChangesAsync();
                return Ok(fileAccessRejectLog);
            }
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<FileAccessRejectLogController> logger;
    }
}
