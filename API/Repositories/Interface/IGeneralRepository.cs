namespace API_CodeFirst.Repositories.Interface;

public interface IGeneralRepository<Entity, Key>
    where Entity : class
{
    Task<IEnumerable<Entity>> GetAllAsync();
    Task<Entity?> GetByIdAsync(Key? key);
    Task<int> InsertAsync(Entity entity);
    Task<int> UpdateAsync(Entity entity);
    Task<int> DeleteAsync(Key key);
    Task<bool> IsDataExist(Key key);
}
