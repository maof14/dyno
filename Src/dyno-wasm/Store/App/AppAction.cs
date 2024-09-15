namespace Store.App;

public class AuthenticateAction
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class SetLoggedInStatusAction
{
    public bool IsLoggedIn { get; set; }
}
