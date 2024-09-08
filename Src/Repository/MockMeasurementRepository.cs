using Models;

namespace Repository;

public interface IRepository<T>
{
    T Get(Guid id);
    List<T> GetAll();
}

public class MockMeasurementRepository : IRepository<Measurement>
{
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
        return Enumerable.Range(1, 50).Select(x => new Measurement()
        {
            Id = Guid.NewGuid(),
            DateTime = DateTimeOffset.Now,
            MeasurementResults = GenerateMeasurementResults(rnd)
        }).ToList();
    }

    private static List<MeasurementResult> GenerateMeasurementResults(Random rnd)
    {
        return Enumerable.Range(0, 99)
                         .Select(x => new MeasurementResult { Id = Guid.NewGuid(), DataPoint = rnd.Next(1, 100), Count = x, DateTimeRecorded = DateTime.UtcNow })
                         .ToList();
    }
}



