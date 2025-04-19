public class RegisterResponse
{
    public bool success { get; set; }
    public string Message { get; set; } = string.Empty;

    public RegisterResponse(bool success, string message){
        this.success = success;
        Message = message;
    }

    public static RegisterResponse Failure(string message) => new RegisterResponse(false, message);
    public static RegisterResponse Success(string message) => new RegisterResponse(true,  message);
}