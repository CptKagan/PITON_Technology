using System.ComponentModel.DataAnnotations;

public class TaskUpdateRequest{
    public string? Title {get; set;}
    public string? Description {get; set;}
    [Range(0,2,ErrorMessage = "Period must be 0 (Daily), 1 (Weekly) or 2 (Monthly).")]
    public TaskDateEnum? Period {get; set;}
    public bool? Completed {get; set;}
}