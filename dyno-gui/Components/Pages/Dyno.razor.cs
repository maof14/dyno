using dyno_gui.SignalR;
using Microsoft.AspNetCore.Components;

namespace dyno_gui.Components.Pages
{
    public partial class Dyno
    {
        [Inject]
        public IHubClient HubClient { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await HubClient.ConnectAsync();

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}