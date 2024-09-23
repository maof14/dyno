using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.App;

namespace dyno_wasm.Pages;

public partial class Home : IDisposable
{
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    public IState<AppState> AppState { get; set; }

    [Inject]
    public IActionSubscriber Subscriber { get; set; }

    public void Dispose()
    {
        Subscriber.UnsubscribeFromAllActions(this);
    }

    protected override void OnInitialized()
    {
        Subscriber.SubscribeToAction<SetRegisteringAvailableAction>(this, (action) =>
        {
            StateHasChanged();
        });

        // todo move to nav or somewhre "global"
        Dispatcher.Dispatch(new InitHomeAction());

        base.OnInitialized();
    }
}