using Fluxor;
using Store.App;

namespace Store.App_wasm.Store.App;

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
