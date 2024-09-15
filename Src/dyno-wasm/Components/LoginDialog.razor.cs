using Microsoft.AspNetCore.Components;
using MudBlazor;
using ViewModels;

namespace dyno_wasm.Components;

public partial class LoginDialog
{
    public LoginModel LoginModel { get; set; } = new();

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; }

    private void Submit() => MudDialog.Close(DialogResult.Ok(LoginModel));

    private void Cancel() => MudDialog.Cancel();
}