using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
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

        [HttpGet("{id:int}")]
        public async Task<Whitelist> Get(int id)
        {
            return await dbContext.Whitelist
                .Where(list => (list.Id == id))
                .FirstOrDefaultAsync();
        }

        [HttpGet("latest")]
        public async Task<Whitelist> GetLatest()
        {
            return await dbContext.Whitelist
                .OrderByDescending(list => list.Id)
                .FirstOrDefaultAsync();
        }

        [HttpGet("distribute/version:regex(^\\d{{1}}.\\d{{1}}.\\d{{1}}$)")]
        public async Task<IActionResult> Distribute(int version)
        {
            // sftp를 이용해 화이트리스트 전송
            await Task.Run(() => {
                batchFileSender.Start();
                batchFileSender.WaitForExit();
            });
            // TODO: TCP를 이용해 에이전트 프로그램에 알림 (홈 화면)
            return Ok();
        }

        private readonly SqliteDbContext dbContext;

        private readonly ILogger<WhitelistController> logger;

        private readonly Process batchFileSender = new Process()
        {
            StartInfo = new ProcessStartInfo("file_sender.bat")
            {
                UseShellExecute = false
            }
        };
    }
}
