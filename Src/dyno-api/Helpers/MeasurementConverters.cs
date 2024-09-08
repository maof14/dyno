using Models;
using ViewModels;

namespace dyno_api.Helpers;

internal static class MeasurementConverters
{
    internal static MeasurementModel Convert(Measurement x)
    {
        return new MeasurementModel(
            id: x.Id,
            dateTime: x.DateTime,
            measurementResults: x.MeasurementResults.Select(y => new MeasurementResultModel(id: y.Id, datapoint: y.DataPoint, count: y.Count, dateTimeRecorded: y.DateTimeRecorded))
            .ToList());
    }
}