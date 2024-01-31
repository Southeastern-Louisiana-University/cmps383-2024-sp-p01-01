using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Dto;


namespace Selu383.SP24.Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
