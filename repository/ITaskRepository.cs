public interface ITaskRepository{
    Task<TaskItem> AddTaskAsync(TaskItem taskItem);
    Task SaveChangesAsync();

    Task <List<TaskItem>> GetAllAsync(long userId, int pageNumber, int pageSize, TaskDateEnum? period, bool? completed);
    Task <TaskItem?> GetTaskByIdAsync(long id);
    Task DeleteTaskAsync(TaskItem taskItem);
}