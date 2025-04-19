
using Microsoft.EntityFrameworkCore;
using PITON_Project.Data;

public class TaskRepository : ITaskRepository
{

    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<TaskItem> AddTaskAsync(TaskItem taskItem)
    {
        await _context.Tasks.AddAsync(taskItem);
        return taskItem;
    }
    public async Task<List<TaskItem>> GetAllAsync(long userId, int pageNumber, int pageSize)
    {
        return await _context.Tasks
            .Include(t => t.User)
            .Where(t => t.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(t => t.CreatedTime)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(long id)
    {
        return await _context.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
    }

    public Task DeleteTaskAsync(TaskItem taskItem)
    {
        _context.Tasks.Remove(taskItem);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }


}