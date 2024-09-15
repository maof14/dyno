using dyno_wasm.Components;
using Fluxor;
using Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using Store.App;
using Store.Measurements;
using ViewModels;

namespace dyno_wasm.Layout;

public partial class NavMenu : IDisposable
{
    private bool collapseNavMenu = true;
    private bool disposedValue;

    [Inject]
    public IDialogService DialogService { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    public IState<AppState> AppState { get; set; }

    [Inject]
    IActionSubscriber ActionSubscriber { get; set; }

    public bool IsLoggedIn => AppState.Value.IsLoggedIn;

    private string NavMenuCssClass => collapseNavMenu ? "collapse" : "nocollapse";

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    protected override void OnInitialized()
    {
        ActionSubscriber.SubscribeToAction<SetLoggedInStatusAction>(this, (action) =>
        {
            StateHasChanged();
        });
    }

    private async Task OnLoginClick()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };

        var dialog = await DialogService.ShowAsync<LoginDialog>("Ree", options);
        var result = await dialog.Result;

        if (result.Canceled)
        {
            return;
        }

        var loginModel = result.Data.As<LoginModel>();

        Dispatcher.Dispatch(new AuthenticateAction() { Password = loginModel.Password, Username = loginModel.Username});
    }

    private void OnLogoutClick() {
        Dispatcher.Dispatch(new ClearMeasurementsAction());
        Dispatcher.Dispatch(new DeAuthenticateAction());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            ActionSubscriber.UnsubscribeFromAllActions(this);

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}