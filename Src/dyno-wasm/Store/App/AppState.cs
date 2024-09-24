using Fluxor;

namespace Store.App;

public record AppState
{
    public bool IsLoggedIn { get; set; } = false;
    public bool RegisteringEnabled { get; set; } = false;
    public bool IsLoading { get; set; } = false;
}

public class AppFeature : Feature<AppState>
{
    public override string GetName() => nameof(AppState);

    protected override AppState GetInitialState() => new AppState();
}
