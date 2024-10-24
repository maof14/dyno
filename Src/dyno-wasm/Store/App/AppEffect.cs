using Common;
using Fluxor;
using Service;
using Store.SharedActions;

namespace Store.App;

public class AppEffect
{
    private readonly IClientTokenService _tokenService;

    public AppEffect(IClientTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [EffectMethod]
    public async Task OnAuthenticateAction(AuthenticateAction action, IDispatcher dispatcher)
    {
        try
        {
            dispatcher.Dispatch(new SetLoadingAction { IsLoading = true });
            await _tokenService.GetTokenAsync(action.Username, action.Password);
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new SetLoadingAction { IsLoading = false });
            dispatcher.Dispatch(new ToastSuccessAction() { SuccessMessage = "Logging in went to hell" });
            return;
        }

        dispatcher.Dispatch(new SetLoadingAction() { IsLoading = false });
        dispatcher.Dispatch(new SetLoggedInStatusAction() { IsLoggedIn = true });
    }

    [EffectMethod]
    public async Task OnRegisterAction(RegisterAction action, IDispatcher dispatcher) {
        try {
            dispatcher.Dispatch(new SetLoadingAction { IsLoading = true });
            await _tokenService.RegisterAsync(action.Username, action.Password, action.PasswordRepeat);
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new SetLoadingAction { IsLoading = false });
            dispatcher.Dispatch(new ToastSuccessAction() {SuccessMessage = "Registering did not work."});
            return;
        }

        dispatcher.Dispatch(new SetLoadingAction { IsLoading = false });
        dispatcher.Dispatch(new RegisterSuccessAction());
        dispatcher.Dispatch(new ToastSuccessAction() { SuccessMessage = "Registering successful. You can now login. "});
    }

    [EffectMethod(typeof(DeAuthenticateAction))]
    public async Task OnDeauthenticateAction(IDispatcher dispatcher)
    {
        _tokenService.ResetToken();

        dispatcher.Dispatch(new SetLoggedInStatusAction() { IsLoggedIn = false });
        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task OnInitHomeAction(InitHomeAction action, IDispatcher dispatcher)
    {
        var result = await _tokenService.GetRegisteringAvailableStatus();

        dispatcher.Dispatch(new SetRegisteringAvailableAction() { RegisteringAvailable = result });
    }
}
