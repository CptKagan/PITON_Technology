public class User{
    public long Id {get; set;}
    public string UserName {get; set;}
    public string Email {get; set;}
    public string password{get; set;}
    public string Role {get; set;} = "User";

    public ICollection<TaskItem> TaskItems {get; set;} = new List<TaskItem>();

    public User(){}
    public User(RegisterRequest registerRequest){
        this.UserName = registerRequest.UserName;
        this.Email = registerRequest.Email;
        this.password = registerRequest.Password;
    }
}