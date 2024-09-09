using Models;

namespace Repository;

public interface IRepository<T>
{
    T Get(Guid id);
    List<T> GetAll();
    bool Create(T entity);
}

public class MockMeasurementRepository : IRepository<Measurement>
{
    private const int MockMeasurementsCount = 50;

    // Todo implement with EF
    private List<Measurement> _measurements = new List<Measurement>();

    public MockMeasurementRepository()
    {
        var rnd = new Random();

        List<Measurement> measurements = GenerateMeasurements(rnd);

        _measurements.AddRange(measurements);
    }

    public Measurement Get(Guid id)
    {
        return _measurements.Where(x => x.Id == id).First();
    }

    public List<Measurement> GetAll()
    {
        return _measurements
            .OrderByDescending(x => x.DateTime)
            .ToList();
    }
    private static List<Measurement> GenerateMeasurements(Random rnd)
    {
        return Enumerable.Range(0, 1).Select(x => new Measurement(
            id: Guid.NewGuid(),
            dateTime: DateTimeOffset.Now,
            measurementResults: GenerateMeasurementResults(rnd))).ToList();
    }

    private static List<MeasurementResult> GenerateMeasurementResults(Random rnd)
    {
        return Enumerable.Range(0, MockMeasurementsCount)
            .Select(x => new MeasurementResult(
                id: Guid.NewGuid(),
                datapoint: rnd.Next(1, 100),
                count: x,
                dateTimeRecorded: DateTimeOffset.Now))
            .ToList();
    }

    public bool Create(Measurement entity)
    {
        _measurements.Add(entity);
        return true;
    }
}



