using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository;

public class AppUserRepository : IRepository<AppUserEntity>
{
    private readonly DynoDbContext _dbContext;

    public AppUserRepository(DynoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Create(AppUserEntity entity)
    {
        using (var tx = _dbContext.Database.BeginTransaction())
        {
            try
            {
                await _dbContext.AppUsers.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                await tx.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return false;
            }
        }
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<AppUserEntity> Get(Guid id)
    {
        return await _dbContext
            .AppUsers
            .FirstAsync(x => x.Id == id);
    }

    public async Task<List<AppUserEntity>> GetAll()
    {
        return await _dbContext
            .AppUsers
            .ToListAsync();
    }
}
