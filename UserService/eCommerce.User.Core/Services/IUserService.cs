using eCommerce.User.Core.DTOs;

namespace eCommerce.User.Core.Services;

public interface IUserService
{
    public Task<AuthResponseDto?> Login(LoginDto loginDto);

    public Task<AuthResponseDto?> Register(RegisterDto registerDto);
}
