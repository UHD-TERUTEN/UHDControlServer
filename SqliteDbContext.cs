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
            modelBuilder.Entity<FileAccessRejectLog>()
                .Property(p => p.AgentId)
                .HasDefaultValue(1);
            modelBuilder.Entity<FileAccessRejectLog>()
                .Property(p => p.Date)
                .HasDefaultValueSql("date('now')");
            modelBuilder.Entity<FileAccessRejectLog>()
                .Property(p => p.IsAllowed)
                .HasDefaultValue(false);
            modelBuilder.Entity<FileAccessRejectLog>()
                .HasIndex(p => p.PlainText)
                .IsUnique();

            modelBuilder.Entity<SystemLog>()
                .Property(p => p.AgentId)
                .HasDefaultValue(1);
            modelBuilder.Entity<SystemLog>()
                .Property(p => p.Date)
                .HasDefaultValue(DateTime.Today.ToString("d"));

            modelBuilder.Entity<Whitelist>()
                .Property(p => p.LastDistributed)
                .HasDefaultValue(null);
                
            // var rng = new Random();

            // modelBuilder.Entity<FileAccessRejectLog>().HasData(Enumerable.Range(1, 5)
            //     .Select(index => new FileAccessRejectLog
            //     {
            //         Id = index,
            //         AgentId = rng.Next(1, 10),
            //         Date = DateTime.UtcNow.AddDays(index),
            //         ProgramName = $"program-{index}",
            //         Details = $"details-{index}",
            //         IsAllowed = (rng.Next(1, 2) == 1),
            //     })
            //     .ToArray());

            // modelBuilder.Entity<Whitelist>().HasData(Enumerable.Range(1, 5)
            //     .Select(index => new Whitelist
            //     {
            //         Id = index,
            //         Version = $"1.0.{index - 1}",
            //     }).ToArray());

            // modelBuilder.Entity<SystemLog>().HasData(Enumerable.Range(1, 5)
            //     .Select(index => new SystemLog
            //     {
            //         Id = index,
            //         AgentId = rng.Next(1, 10),
            //         Date = DateTime.UtcNow.AddDays(index),
            //         Size = 1024 * index,
            //     })
            //     .ToArray());
        }

        private static readonly string connectionString = @"Data Source=db.sqlite";

        public DbSet<FileAccessRejectLog> FileAccessRejectLog { get; set; }

        public DbSet<Whitelist> Whitelist { get; set; }

        public DbSet<SystemLog> SystemLog { get; set; }
    }
}
