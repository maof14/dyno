using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.App;
using ViewModels;

namespace dyno_wasm.Pages;

public partial class Register {

    [Inject]
    public IDispatcher Dispatcher {get;set;}
    
    [Inject]
    public IState<AppState> AppState {get;set;}

    private RegisterModel registerModel = new();

    private void HandleRegisterClick() {
        // Dispatch action to register..
    }
}