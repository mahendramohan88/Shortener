using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Shortener.Data.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<UrlMap> UrlMaps { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // The sequence starts at 1000001 to avoid the first short URL being only one character
            modelBuilder.HasSequence<int>("Order_seq", schema: "dbo")
                .StartsAt(1000001)
                .IncrementsBy(1);

            modelBuilder.Entity<UrlMap>()
                .Property(urlMap => urlMap.ID)
                .HasDefaultValueSql("NEXT VALUE FOR dbo.Order_seq");
        }
    }
}
