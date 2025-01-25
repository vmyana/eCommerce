namespace eCommerce.User.Core.DTOs;

public class AuthResponseDto
{
    public Guid UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public string? Token { get; set; }

    public bool IsSuccess { get; set; }
}
