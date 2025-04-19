public class TaskResponse{
    public long Id {get; set;}
    public string? Title {get; set;}
    public string? Description {get; set;}
    public string? CreatedTime {get; set;}
    public string? UpdatedTime {get; set;}
    public string? Period {get; set;}
    public string? DueDate {get; set;}
    public bool Completed {get; set;}
    public long UserId {get; set;}
    public string? UserName {get; set;}

    public TaskResponse(TaskItem taskItem){
        Id = taskItem.Id;
        Title = taskItem.Title;
        Description = taskItem.Description;
        CreatedTime = taskItem.CreatedTime.ToString();
        UpdatedTime = taskItem.UpdatedTime.ToString();
        Period = taskItem.Period.ToString();
        DueDate = taskItem.DueDate.ToString();
        Completed = taskItem.Completed;
        UserId = taskItem.UserId;
        UserName = taskItem.User.UserName;
    }
}