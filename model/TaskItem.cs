using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;

public class TaskItem{
    public long Id {get; set;}
    public string Title {get; set;} = null!;
    public string? Description {get; set;}
    public DateTime CreatedTime {get; set;}
    public DateTime? UpdatedTime {get;set;}
    public TaskDateEnum Period {get; set;}
    public DateTime DueDate {get; set;}
    public bool Completed {get; set;} = false;

    public long UserId {get; set;}
    public User User {get; set;} = null!;

    public TaskItem(){}

    public TaskItem(TaskRequest taskRequest){
        Title = taskRequest.Title;
        Description = taskRequest.Description;
        Period = taskRequest.Period;
        CreatedTime = DateTime.UtcNow;
    }
}