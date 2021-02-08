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
    [Route("api/whitelist")]
    public class WhitelistController : ControllerBase
    {
        public WhitelistController(ILogger<WhitelistController> logger, SqliteDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet("{id:int}")]
        public async Task<Whitelist> Get(int id)
        {
            return await dbContext.Whitelist
                .Where(list => (list.Id == id))
                .FirstOrDefaultAsync();
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public async Task<IEnumerable<Whitelist>> GetPage([FromQuery(Name = "page")] int page)
        {
            return await dbContext.Whitelist.ToListAsync();
        }

        [HttpGet]
        [ExactQueryParam("version")]
        public async Task<IEnumerable<Whitelist>> GetByVersion([FromQuery(Name = "version")] string version)
        {
            return await dbContext.Whitelist
                .Where(list => (list.Version == version))
                .ToListAsync();
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<WhitelistController> logger;
    }
}
