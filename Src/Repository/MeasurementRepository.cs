using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository;

public class MeasurementRepository : IRepository<MeasurementEntity>
{
    private readonly DynoDbContext _dbContext;

    public MeasurementRepository(DynoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MeasurementEntity> Get(Guid id)
    {
        return await _dbContext
            .Measurements
            .Include(x => x.MeasurementResults)
            .FirstAsync(x => x.Id == id);
    }

    public async Task<List<MeasurementEntity>> GetAll()
    {
        return await _dbContext
            .Measurements
            .Include(x => x.MeasurementResults)
            .ToListAsync();
    }

    public async Task<bool> Create(MeasurementEntity entity)
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

    public async Task<bool> Delete(Guid id)
    {
        using (var tx = _dbContext.Database.BeginTransaction())
        {
            var success = false;
            try
            {
                var measurement = await _dbContext.Measurements.FirstAsync(x => x.Id == id);

                if(measurement != null)
                {
                    _dbContext.Measurements.Attach(measurement);
                    _dbContext.Measurements.Remove(measurement);
                    await _dbContext.SaveChangesAsync();
                    await tx.CommitAsync();
                    success = true;
                }
            }
            catch (Exception)
            {
                await tx.RollbackAsync();
            }
            return success;
        }
    }
}



