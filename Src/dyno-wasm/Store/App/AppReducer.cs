using Fluxor;
using Store.App;

namespace Store.App_wasm.Store.App;

public class AppReducer
{
    [ReducerMethod]
    public static AppState OnSetLoggedInStatusAction(SetLoggedInStatusAction action, AppState state)
    {
        return state with
        {
            IsLoggedIn = action.IsLoggedIn
        };
    }
}
