using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using UHDControlServer.Attributes;
using UHDControlServer.Models;
using System.Text.RegularExpressions;

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

            batchFileSender = new Process()
            {
                StartInfo = new ProcessStartInfo("scripts/send.bat")
                {
                    UseShellExecute = false
                }
            };

            validateVersion = new Regex("^\\d{1}.\\d{1}.\\d{1}$");
        }

        [HttpGet]
        [ExactQueryParam("page")]
        public async Task<IActionResult> GetPage([FromQuery(Name = "page")] int page)
        {
            if (page < 1)
                return BadRequest($"Out of range: {page}");

            var whitelists = await dbContext.Whitelist.ToListAsync();
            return Ok(whitelists);
        }

        [HttpGet]
        [ExactQueryParam("version")]
        public async Task<IActionResult> GetByVersion([FromQuery(Name = "version")] string version)
        {
            if (!validateVersion.IsMatch(version))
                return BadRequest($"Invalid version: {version}");

            var whitelist = await dbContext.Whitelist
                .Where(list => (list.Version == version))
                .FirstOrDefaultAsync();
            return Ok(whitelist);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest($"Out of range: {id}");

            var whitelist = await dbContext.Whitelist
                .Where(list => (list.Id == id))
                .FirstOrDefaultAsync();
            return Ok(whitelist);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var latest = await dbContext.Whitelist
                .OrderByDescending(list => list.Id)
                .FirstOrDefaultAsync();
            return Ok(latest);
        }

        [HttpGet("distribute")]
        [ExactQueryParam("version")]
        public async Task<IActionResult> Distribute([FromQuery(Name = "version")] string version)
        {
            if (!validateVersion.IsMatch(version))
                return BadRequest($"Invalid version: {version}");

            var exists = await dbContext.Whitelist
                .AnyAsync(list => (list.Version == version));

            if (!exists)
                return NoContent();

            // sftp를 이용해 화이트리스트 전송
            await Task.Run(() =>
            {
                if (batchFileSender.Start())
                    batchFileSender.WaitForExit();
            });
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Whitelist whitelist)
        {
            var whitelistEntry = dbContext.Whitelist.Update(whitelist);
            var changedEntry = whitelistEntry.Context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            if (changedEntry.Count != 1)
            {
                whitelistEntry.Reload();
                return BadRequest($"Invalid data: {whitelist}");
            }
            else
            {
                await dbContext.SaveChangesAsync();
                return Ok(whitelist);
            }
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<WhitelistController> logger;

        private Process batchFileSender;

        private readonly Regex validateVersion;
    }
}
