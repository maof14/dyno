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

    public Measurement Get(Guid id)
    {
        return _measurements.Where(x => x.Id == id).First();
    }

    public List<Measurement> GetAll()
    {
        return _measurements
            .OrderByDescending(x => x.DateTimeOffset)
            .ToList();
    }

    public bool Create(Measurement entity)
    {
        _measurements.Add(entity);
        return true;
    }
}



