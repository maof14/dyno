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

    [ReducerMethod]
    public static AppState OnSetRegisteringAvailableAction(AppState state, SetRegisteringAvailableAction action)
    {
        return state with
        {
            RegisteringEnabled = action.RegisteringAvailable
        };
    } 

    [ReducerMethod]
    public static AppState SetLoadingAction(AppState state, SetLoadingAction action)
    {
        return state with
        {
            IsLoading = action.IsLoading
        };
    }
}
