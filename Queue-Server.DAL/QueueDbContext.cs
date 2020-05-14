using Microsoft.EntityFrameworkCore;
using Queue_Server.Common.Enums;
using Queue_Server.Common.Models;
using System;

namespace Queue_Server.DAL
{
    public class QueueDbContext : DbContext
    {
        public QueueDbContext(DbContextOptions<QueueDbContext> options) : base(options)
        {

        }

        public DbSet<QueueEntity> QueueEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<QueueEntity>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));
        }
    }
}
