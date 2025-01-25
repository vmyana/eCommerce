namespace eCommerce.User.Core.Models;

public class ApplicationUser
{
    public Guid UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Gender { get; set; }
}
