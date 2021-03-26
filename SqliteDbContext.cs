using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UHDControlServer.Models;

namespace UHDControlServer
{
    public class SqliteDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var rng = new Random();

            modelBuilder.Entity<FileAccessRejectLog>().HasData(Enumerable.Range(1, 5)
                .Select(index => new FileAccessRejectLog
                {
                    Id = index,
                    AgentId = rng.Next(1, 10),
                    DateTime = DateTime.UtcNow.AddDays(index),
                    ProgramName = $"program-{index}",
                    Details = $"details-{index}",
                    IsAllowed = (rng.Next(1, 2) == 1),
                    Inquiries = new Inquiry[] { },
                })
                .ToArray());

            modelBuilder.Entity<Whitelist>().HasData(Enumerable.Range(1, 5)
                .Select(index => new Whitelist
                {
                    Id = index,
                    Version = $"1.0.{index - 1}",
                    LastUpdated = DateTime.UtcNow.AddDays(index - 5),
                    LastDistributed = DateTime.UtcNow,
                }).ToArray());

            modelBuilder.Entity<SystemLog>().HasData(Enumerable.Range(1, 5)
                .Select(index => new SystemLog
                {
                    Id = index,
                    AgentId = rng.Next(1, 10),
                    DateTime = DateTime.UtcNow.AddDays(index),
                    Size = 1024 * index,
                })
                .ToArray());
        }

        private static readonly string connectionString = @"Data Source=db.sqlite";

        public DbSet<FileAccessRejectLog> FileAccessRejectLogs { get; set; }

        public DbSet<Whitelist> Whitelist { get; set; }

        public DbSet<SystemLog> SystemLogs { get; set; }
    }
}
