using Microsoft.AspNetCore.Identity.Data;

public interface IUserService{
    Task<RegisterResponse> CreateAsync(RegisterRequest registerRequest);
    Task<string?> LoginAsync(LoginRequest loginRequest);

    Task<User?> FindUserByIdAsync(long userid);
    
}