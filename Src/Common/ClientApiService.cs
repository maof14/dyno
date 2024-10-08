﻿using System.Net.Http.Json;
using ViewModels;

namespace Common;

public interface IClientApiService
{
    Task CreateMeasurement(MeasurementModel result);
    Task DeleteMeasurement(Guid id);
    Task<List<MeasurementModel>> GetMeasurementModels();
}

public class ClientApiService : IClientApiService
{
    private readonly HttpClient httpClient;

    public ClientApiService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("APIClient");
    }

    public async Task CreateMeasurement(MeasurementModel result)
    {
        var response = await httpClient.PostAsJsonAsync(Routes.Measurements, result);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error posting result");
        }
    }

    public async Task DeleteMeasurement(Guid id)
    {
        var response = await httpClient.DeleteAsync($"{Routes.Measurements}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error deleting result");
        }
    }

    public async Task<List<MeasurementModel>> GetMeasurementModels()
    {
        var response = await httpClient.GetFromJsonAsync<List<MeasurementModel>>(Routes.Measurements);

        return response;
    }
}
