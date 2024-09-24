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

public class RegisterAction {
    public string Username {get;set;}
    public string Password {get;set;}
    public string PasswordRepeat {get;set;}
}

public class DeAuthenticateAction { }

public class InitHomeAction { }

public class SetRegisteringAvailableAction { 
    public bool RegisteringAvailable { get; set; }  
}

public class RegisterSuccessAction { }

public class SetLoadingAction { 
    public bool IsLoading { get; set; }
}
