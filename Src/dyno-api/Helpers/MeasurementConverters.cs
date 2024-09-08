using Models;
using ViewModels;

namespace dyno_api.Helpers;

internal static class MeasurementConverters
{
    internal static MeasurementModel Convert(Measurement measurement)
    {
        return new MeasurementModel(
            id: measurement.Id,
            dateTime: measurement.DateTime,
            measurementResults: measurement.MeasurementResults.Select(y => new MeasurementResultModel(id: y.Id, datapoint: y.Datapoint, count: y.Count, dateTimeRecorded: y.DateTimeRecorded))
            .ToList());
    }

    internal static Measurement Convert(MeasurementModel measurementModel)
    {
        return new Measurement(
            id: measurementModel.Id,
            dateTime: measurementModel.DateTime,
            measurementResults: measurementModel.MeasurementResults.Select(y => new MeasurementResult(id: y.Id, datapoint: y.Datapoint, count: y.Count, dateTimeRecorded: y.DateTimeRecorded))
            .ToList());
    }
}