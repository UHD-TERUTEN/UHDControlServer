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
    [Route("api/system-logs")]
    public class SystemLogController : ControllerBase
    {
        public SystemLogController(ILogger<SystemLogController> logger, SqliteDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet("{id:int}")]
        public async Task<SystemLog> Get(int id)
        {
            return await dbContext.SystemLogs
                .Where(log => (log.Id == id))
                .FirstOrDefaultAsync();
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public async Task<IEnumerable<SystemLog>> GetPage([FromQuery(Name = "page")] int page)
        {
            return await dbContext.SystemLogs.ToListAsync();
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<SystemLogController> logger;
    }
}
