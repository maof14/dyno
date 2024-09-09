using Models;
using ViewModels;

namespace dyno_api.Helpers;

internal static class MeasurementConverters
{
    internal static MeasurementModel Convert(Measurement measurement)
    {
        return new MeasurementModel(
            id: measurement.Id,
            name: measurement.Name,
            description: measurement.Description,
            dateTime: measurement.DateTimeOffset,
            measurementResults: measurement.MeasurementResults.Select(y => new MeasurementResultModel(id: y.Id, torque: y.Torque, rPM: y.RPM, horsepower: y.Horsepower, dateTimeOffset: y.DateTimeOffset))
            .ToList());
    }

    internal static Measurement Convert(MeasurementModel measurementModel)
    {
        return new Measurement(
            id: measurementModel.Id,
            name: measurementModel.Name,
            description: measurementModel.Description,
            dateTimeOffset: measurementModel.DateTime,
            measurementResults: measurementModel.MeasurementResults.Select(y => new MeasurementResult(id: y.Id, torque: y.Torque, rPM: y.RPM, horsepower: y.Horsepower, dateTimeOffset: y.DateTimeOffset))
            .ToList());
    }
}