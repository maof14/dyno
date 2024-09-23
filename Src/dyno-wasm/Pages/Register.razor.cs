using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.App;
using ViewModels;

namespace dyno_wasm.Pages;

public partial class Register : IDisposable {

    [Inject]
    public IDispatcher Dispatcher {get;set;}
    
    [Inject]
    public IState<AppState> AppState {get;set;}

    private RegisterModel registerModel = new();

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IActionSubscriber ActionSubscriber { get; set; }

    protected override void OnInitialized()
    {
        ActionSubscriber.SubscribeToAction<RegisterSuccessAction>(this, (action) =>
        {
            NavigationManager.NavigateTo("/");
        });
        base.OnInitialized();
    }

    private void HandleRegisterClick() {
        Dispatcher.Dispatch(new RegisterAction { Username = registerModel.Username, Password = registerModel.Password, PasswordRepeat = registerModel.PasswordRepeat });
    }

    public void Dispose()
    {
        ActionSubscriber.UnsubscribeFromAllActions(this);
    }
}