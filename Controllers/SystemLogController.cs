using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UHDControlServer.Attributes;
using UHDControlServer.Models;

namespace UHDControlServer.Controllers
{
    [ApiController]
    [Route("api/system-logs")]
    public class SystemLogController : ControllerBase
    {
        public SystemLogController(ILogger<SystemLogController> logger, SqliteDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public async Task<IEnumerable<SystemLog>> GetPage([FromQuery(Name = "page")] int page)
        {
            return await dbContext.SystemLogs.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<SystemLog> Get(int id)
        {
            return await dbContext.SystemLogs
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();
        }

        [HttpGet("{fileName:regex(^[[0-9-_]]{{12}}.zip$)}")]
        public FileResult GetBlobDownload(string fileName)
        {
            string filePath = $"../logs/{fileName}";
            var content = System.IO.File.OpenRead(filePath);
            var contentType = "application/octet-stream";
            return File(content, contentType, fileName);
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<SystemLogController> logger;
    }
}
