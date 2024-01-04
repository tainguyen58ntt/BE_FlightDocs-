

using FlightDocs.Service.DocumentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Serivce.DocumentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentPermissions> DocumentPermissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
        }

    }
}
