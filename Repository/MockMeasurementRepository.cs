using Models;

namespace Repository;

public class MockMeasurementRepository : IRepository<Measurement>
{
    // Todo implement with EF
    private List<Measurement> _measurements = new List<Measurement>();

    public MockMeasurementRepository()
    {
        var measurements = new List<Measurement>()
        {
            new Measurement() { Id = Guid.NewGuid() ,
            MeasurementResults = new List<MeasurementResult>
                {
                    new MeasurementResult { Id = Guid.NewGuid(), DataPoint = 5, DateTimeRecorded = DateTime.UtcNow }
                }
            } 
        };

        _measurements.AddRange(measurements);          
    }

    public Measurement Get(Guid id)
    {
        return _measurements.Where(x => x.Id == id).First();
    }

    public List<Measurement> GetAll()
    {
        return _measurements.ToList();
    }
}

public interface IRepository<T> 
{
    T Get(Guid id);
    List<T> GetAll();
}
