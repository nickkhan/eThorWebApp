using eThorWebApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Linq;

namespace eThorWebApp.Shared.Data
{
    public class eThorTestEntityContext : DbContext
    {
        public eThorTestEntityContext()
        {

        }
        public eThorTestEntityContext(DbContextOptions<eThorTestEntityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var splitStringConverter =
                new ValueConverter<List<string>, string>(
                    v => string.Join(",", v),
                    v => v.Split(new[] { ',' }).ToList());

            modelBuilder.Entity<eThorTestEntity>()
            .Property(nameof(eThorTestEntity.HardPropertyList))
            .HasConversion(splitStringConverter);
        }
        public DbSet<eThorTestEntity> eThorTestEntites { get; set; }
    }
}
