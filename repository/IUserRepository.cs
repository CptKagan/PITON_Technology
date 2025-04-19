public interface IUserRepository{
    Task<User> AddUserAsync(User user);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(string email);
    Task SaveChangesAsync();

    Task<User?> FindByUsernameAsync(string username);
    Task<User?> FindByIdAsync(long userid);
}