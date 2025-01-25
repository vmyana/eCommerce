using eCommerce.User.Core.Models;

namespace eCommerce.User.Core.Repositories;

public interface IUserRepository
{
    public Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string? email, string? password);

    public Task<ApplicationUser?> AddUserAsync(ApplicationUser user);
}
