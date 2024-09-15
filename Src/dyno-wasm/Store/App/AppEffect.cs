﻿using Common;
using Fluxor;
using Store.SharedActions;

namespace Store.App;

public class AppEffect
{
    private readonly ITokenService _tokenService;

    public AppEffect(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [EffectMethod]
    public async Task OnAuthenticateAction(AuthenticateAction action, IDispatcher dispatcher)
    {
        try
        {
            await _tokenService.GetTokenAsync(action.Username, action.Password);
        }
        catch (Exception)
        {
            dispatcher.Dispatch(new ToastSuccessAction() { SuccessMessage = "Logging in went to hell" });
            return;
        }

        dispatcher.Dispatch(new SetLoggedInStatusAction() { IsLoggedIn = true });
    }

}
