using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace eCommerce.User.Infrastructure.DbContext;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;
    
    public IDbConnection DbConnection  => _dbConnection;

    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
}
