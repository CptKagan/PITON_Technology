public class CurrentUserService : ICurrentUserService
{
    public long UserId {get;}
    public string? UserName {get;}

    public CurrentUserService(IHttpContextAccessor http){
        var user = http.HttpContext?.User;
        if(user?.Identity?.IsAuthenticated ?? false){
            UserName = user.Identity.Name;
            UserId = long.Parse(user.FindFirst("userid")!.Value);
        }
    }
}