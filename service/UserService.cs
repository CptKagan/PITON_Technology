using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<RegisterResponse> CreateAsync(RegisterRequest registerRequest)
    {
        if(await _userRepository.ExistsByUsernameAsync(registerRequest.UserName)){
            return RegisterResponse.Failure("This username already exists!");
        }
        if(await _userRepository.ExistsByEmailAsync(registerRequest.Email)){
            return RegisterResponse.Failure("This email already exists!");
        }

        User user = new User(registerRequest);

        user.password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.password,13);
        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();
        return RegisterResponse.Success("Account created successfully!");
    }

    public async Task<User?> FindUserByIdAsync(long userid)
    {
        return await _userRepository.FindByIdAsync(userid);
    }

    public async Task<string?> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userRepository.FindByUsernameAsync(loginRequest.UserName);

        if(user == null || !BCrypt.Net.BCrypt.EnhancedVerify(loginRequest.Password, user.password)){
            return null;
        }

        var token = _tokenService.CreateToken(user);
        return token;
    }
}