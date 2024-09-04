﻿using Common;
using ViewModels;

namespace dyno_gui.Client;

public interface IClientApiService
{
    Task<List<MeasurementModel>> GetMeasurementModels();
}

public class ClientApiService : IClientApiService
{
    private readonly HttpClient httpClient;

    public ClientApiService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("APIClient");
    }

    public async Task<List<MeasurementModel>> GetMeasurementModels()
    {
        var response = await httpClient.GetFromJsonAsync<List<MeasurementModel>>(Routes.Measurements);

        return response;
    }
}
