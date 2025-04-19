using System.ComponentModel.DataAnnotations;

public class TaskRequest{
    [Required(ErrorMessage = "Task Title is required")]
    [MinLength(5, ErrorMessage = "Task Title must be atleast 5 characters long.")]
    public required string Title {get; set;}
    public string? Description {get; set;}
    [Required(ErrorMessage = "Period is required")]
    [Range(0,2,ErrorMessage = "Period must be 0 (Daily), 1 (Weekly) or 2 (Monthly).")]
    public TaskDateEnum Period {get; set;}
}