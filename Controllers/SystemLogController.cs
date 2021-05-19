using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UHDControlServer.Attributes;
using UHDControlServer.Models;

namespace UHDControlServer.Controllers
{
    [ApiController]
    [Route("api/system-log")]
    public class SystemLogController : ControllerBase
    {
        public SystemLogController(ILogger<SystemLogController> logger, SqliteDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            validateFileName = new Regex("^[0-9-_]{12}.zip$");
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public async Task<IActionResult> GetPage([FromQuery(Name = "page")] int page)
        {
            if (page < 1)
                return BadRequest($"Out of range: {page}");

            var systemLog = await dbContext.SystemLog.ToListAsync();
            return Ok(systemLog);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest($"Out of range: {id}");

            var systemLog = await dbContext.SystemLog
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();
            return Ok(systemLog);
        }

        [HttpGet("{fileName}")]
        public IActionResult GetBlobDownload(string fileName)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                + $@"\Logs\EventLogs\{fileName}";

            if (!validateFileName.IsMatch(fileName)
                || !System.IO.File.Exists(filePath))
            {
                return BadRequest($"Invalid file name: {fileName}");
            }
            var content = System.IO.File.OpenRead(filePath);
            var contentType = "application/octet-stream";
            return File(content, contentType, fileName);
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<SystemLogController> logger;

        private readonly Regex validateFileName;
    }
}
