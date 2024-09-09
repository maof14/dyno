using dyno_wasm.Store.SharedActions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace dyno_wasm.Components;

public partial class Toast
{
    [Inject]
    public ISnackbar SnackbarService { get; set; }

    protected override void OnInitialized()
    {
        SubscribeToAction<ToastSuccessAction>(ShowSuccessToast);
        base.OnInitialized();
    }

    public void ShowSuccessToast(ToastSuccessAction action)
    {
        SnackbarService.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
        SnackbarService.Add(action.SuccessMessage);
    }
}