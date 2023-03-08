using API.Contexts;
using API_CodeFirst.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API_CodeFirst.Repositories;

public class GeneralRepository<Entity, Key> : IGeneralRepository<Entity, Key>
    where Entity : class
{
    private readonly MyContext context;

    public GeneralRepository(MyContext context)
    {
        this.context = context;
    }

    public async Task<int> DeleteAsync(Key key)
    {
        var entity = await GetByIdAsync(key);
        if (entity is null)
        {
            return 0;
        }

        context.Set<Entity>().Remove(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        return await context.Set<Entity>().ToListAsync();
    }

    public async Task<Entity?> GetByIdAsync(Key? key)
    {
        return await context.Set<Entity>().FindAsync(key);
    }

    public async Task<int> InsertAsync(Entity entity)
    {
        await context.Set<Entity>().AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<bool> IsDataExist(Key key)
    {
        bool result = await context.Set<Entity>().FindAsync(key) is not null;
        context.ChangeTracker.Clear();
        return result;
    }

    public async Task<int> UpdateAsync(Entity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        return await context.SaveChangesAsync();
    }
}
