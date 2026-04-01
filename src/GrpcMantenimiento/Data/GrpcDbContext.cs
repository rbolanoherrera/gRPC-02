using Microsoft.EntityFrameworkCore;

namespace GrpcMantenimiento.Data
{
    public class GrpcDbContext : DbContext
    {
        public GrpcDbContext(DbContextOptions<GrpcDbContext> options) : base(options)
        {
            
        }

        public DbSet<Models.Product> Products { get; set; }
    }
}