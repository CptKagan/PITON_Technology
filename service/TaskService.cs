

public class TaskService : ITaskService
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITaskRepository _taskRepository;

    public TaskService(IUserService userService, ICurrentUserService currentUserService, ITaskRepository taskRepository)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        _taskRepository = taskRepository;
    }

    public async Task<TaskResponse?> AddTask(TaskRequest taskRequest)
    {
        var task = new TaskItem(taskRequest);
        switch (taskRequest.Period)
        {
            case TaskDateEnum.Daily:
                task.DueDate = DateTime.UtcNow.AddDays(1);
                break;
            case TaskDateEnum.Weekly:
                task.DueDate = DateTime.UtcNow.AddDays(7);
                break;
            case TaskDateEnum.Monthly:
                task.DueDate = DateTime.UtcNow.AddMonths(1);
                break;
        }

        task.UserId = _currentUserService.UserId;
        var user = await _userService.FindUserByIdAsync(task.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        task.User = user;

        await _taskRepository.AddTaskAsync(task);
        await _taskRepository.SaveChangesAsync();

        return new TaskResponse(task);
    }

    public async Task<TaskResponse> GetTaskByIdAsync(long id)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null)
        {
            throw new Exception("Task not found!");
        }

        if (task.UserId != _currentUserService.UserId)
        {
            throw new Exception("Task not found!");
        }

        return new TaskResponse(task);
    }

    public async Task<List<TaskResponse>> GetTasks(int pageNumber, int pageSize, TaskDateEnum? period, bool? completed)
    {
        var userId = _currentUserService.UserId;

        List<TaskItem> taskItems = await _taskRepository.GetAllAsync(userId, pageNumber, pageSize, period, completed);
        List<TaskResponse> response = new List<TaskResponse>();
        foreach (var item in taskItems)
        {
            response.Add(new TaskResponse(item));
        }

        return response;
    }

    public async Task<TaskResponse> UpdateTaskAsync(long id, TaskUpdateRequest taskUpdateRequest)
    {
        var userId = _currentUserService.UserId;
        var taskItem = await _taskRepository.GetTaskByIdAsync(id);
        if (taskItem == null || taskItem.UserId != userId)
        {
            throw new Exception("Task not found!");
        }
        bool isUpdated = false;
        if (taskUpdateRequest.Title != null && taskUpdateRequest.Title != taskItem.Title)
        {
            taskItem.Title = taskUpdateRequest.Title;
            isUpdated = true;
        }
        if (taskUpdateRequest.Description != null && taskUpdateRequest.Description != taskItem.Description)
        {
            taskItem.Description = taskUpdateRequest.Description;
            isUpdated = true;
        }
        if (taskUpdateRequest.Period != null && taskUpdateRequest.Period != taskItem.Period)
        {
            taskItem.Period = taskUpdateRequest.Period.Value;
            switch (taskItem.Period)
            {
                case TaskDateEnum.Daily:
                    taskItem.DueDate = taskItem.CreatedTime.AddDays(1);
                    break;
                case TaskDateEnum.Weekly:
                    taskItem.DueDate = taskItem.CreatedTime.AddDays(7);
                    break;
                case TaskDateEnum.Monthly:
                    taskItem.DueDate = taskItem.CreatedTime.AddMonths(1);
                    break;
            }
            isUpdated = true;
        }
        if (taskUpdateRequest.Completed.HasValue && taskUpdateRequest.Completed != taskItem.Completed)
        {
            taskItem.Completed = taskUpdateRequest.Completed.Value;
            isUpdated = true;
        }

        if (!isUpdated)
        {
            throw new InvalidOperationException("Nothing changed. No fields provided for update");
        }

        taskItem.UpdatedTime = DateTime.UtcNow;

        await _taskRepository.SaveChangesAsync();

        return new TaskResponse(taskItem);
    }

    public async Task DeleteTaskAsync(long id){
        var userId = _currentUserService.UserId;
        var taskItem = await _taskRepository.GetTaskByIdAsync(id);

        if(taskItem == null || taskItem.UserId != userId){
            throw new Exception("Task not found!");
        }

        await _taskRepository.DeleteTaskAsync(taskItem);
        await _taskRepository.SaveChangesAsync();
    }
}