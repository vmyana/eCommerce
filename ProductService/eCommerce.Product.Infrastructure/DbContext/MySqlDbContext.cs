using Microsoft.EntityFrameworkCore;

namespace eCommerce.Product.Infrastructure.DbContext;

public class MySqlDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { }

    public DbSet<Models.Product> products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
