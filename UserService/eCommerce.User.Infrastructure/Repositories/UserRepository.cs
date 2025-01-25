using Dapper;
using eCommerce.User.Core.DTOs.Enums;
using eCommerce.User.Core.Models;
using eCommerce.User.Core.Repositories;
using eCommerce.User.Infrastructure.DbContext;

namespace eCommerce.User.Infrastructure.Repositories;

public class UserRepository(DapperDbContext dbContext) : IUserRepository
{
    public async Task<ApplicationUser?> AddUserAsync(ApplicationUser user)
    {
        user.UserId = Guid.NewGuid();

        // SQL Query to insert user data into the "Users" table.
        string query = "INSERT INTO public.\"users\"(\"userid\", \"email\", \"username\", \"gender\", \"password\") VALUES(@UserID, @Email, @UserName, @Gender, @Password)";
        int rowCountAffected = await dbContext.DbConnection.ExecuteAsync(query, user);
        
        if (rowCountAffected > 0 )
        {
            return user;
        }

        return null;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string? email, string? password)
    {
        //SQL query to select a user by Email and Password
        string query = "SELECT * FROM public.\"users\" WHERE \"email\"=@Email AND \"password\"=@Password";
        var parameters = new { Email = email, Password = password };

        ApplicationUser? user = await dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

        return user;
    }
}
