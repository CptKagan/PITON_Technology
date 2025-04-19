using Microsoft.EntityFrameworkCore;
using PITON_Project.Data;

public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<User> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return user;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.UserName == username);
    }

    public async Task<User?> FindByIdAsync(long userid)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Id == userid);
    }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}