using AutoMapper;
using eCommerce.User.Core.DTOs;
using eCommerce.User.Core.Models;
using eCommerce.User.Core.Repositories;

namespace eCommerce.User.Core.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<AuthResponseDto?> Login(LoginDto loginDto)
    {
        var user = await userRepository.GetUserByEmailAndPasswordAsync(loginDto.Email, loginDto.Password);

        if (user == null)
        {
            return null;
        }

        var authResponse = mapper.Map<AuthResponseDto>(user);
        authResponse.IsSuccess = true;
        authResponse.Token = "token";

        return authResponse;
    }

    public async  Task<AuthResponseDto?> Register(RegisterDto registerDto)
    {
        var user = await userRepository.AddUserAsync(mapper.Map<ApplicationUser>(registerDto));;

        if (user == null)
        {
            return null;
        }

        var authResponse = mapper.Map<AuthResponseDto>(user);
        authResponse.IsSuccess = true;
        authResponse.Token = "token";

        return authResponse;
    }
}
