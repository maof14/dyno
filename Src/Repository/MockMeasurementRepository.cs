using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository;

public interface IRepository<T>
{
    Task<T> Get(Guid id);
    Task<List<T>> GetAll();
    Task<bool> Create(T entity);
}

public class MockMeasurementRepository : IRepository<Measurement>
{
    private readonly DynoDbContext _dbContext;

    public MockMeasurementRepository(DynoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Measurement> Get(Guid id)
    {
        return await _dbContext
            .Measurements
            .Include(x => x.MeasurementResults)
            .FirstAsync(x => x.Id == id);
    }

    public async Task<List<Measurement>> GetAll()
    {
        return await _dbContext
            .Measurements
            .Include(x => x.MeasurementResults)
            .ToListAsync();
    }

    public async Task<bool> Create(Measurement entity)
    {
        using (var tx = _dbContext.Database.BeginTransaction())
        {
            try
            {
                await _dbContext.Measurements.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                await tx.CommitAsync();
                return true;
            }
            catch (Exception ex) {
                await tx.RollbackAsync();
                return false;
            }

        }
    }
}



