using BoilerPlate.Domain.Entities.PKFields;
using BoilerPlate.Domain.Entities.PKSourceService;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace BoilerPlate.Infrastructure
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<SourceService> SourceServices { get; set; }
        public DbSet<FieldMapping> FieldMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura el índice único en el campo "Name".
            modelBuilder.Entity<SourceService>()
                .HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}
