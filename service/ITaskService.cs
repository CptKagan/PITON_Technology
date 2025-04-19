public interface ITaskService{
    Task<TaskResponse?> AddTask(TaskRequest taskRequest);
    Task <List<TaskResponse>> GetTasks(int pageNumber, int pageSize, TaskDateEnum? period, bool? completed);

    Task<TaskResponse> GetTaskByIdAsync(long id);

    Task<TaskResponse> UpdateTaskAsync(long id, TaskUpdateRequest taskUpdateRequest);
    Task DeleteTaskAsync(long id);
}