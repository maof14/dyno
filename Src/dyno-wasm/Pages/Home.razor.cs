using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.App;

namespace dyno_wasm.Pages;

public partial class Home
{
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        Dispatcher.Dispatch(new InitHomeAction());
    }
}