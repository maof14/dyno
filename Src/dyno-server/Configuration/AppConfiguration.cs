namespace dyno_server.Configuration;

public class AppConfiguration
{
    public string ApiAddress {  get; set; }
    public string HubBaseAddress { get; set; }
    public string ServerUser { get; set; }
    public string ServerPassword { get; set; }

    public int MaxHubConnectionRetries { get; set; }
    public int MaxHubConnectionRetryDelayInSeconds { get; set; }
}
