using Fluxor;

namespace Store.App;

public class AppReducer
{
    [ReducerMethod]
    public static AppState OnSetLoggedInStatusAction(AppState state, SetLoggedInStatusAction action)
    {
        return state with
        {
            IsLoggedIn = action.IsLoggedIn
        };
    }
}
