public class LoginResponse
{
    public bool success { get; set; }
    public string Message { get; set; } = string.Empty;


    public LoginResponse(bool success, string message){
        this.success = success;
        Message = message;
    }

    public static LoginResponse Failure(string message) => new LoginResponse(false, message);
    public static LoginResponse Success(string message) => new LoginResponse(true,  message);
}