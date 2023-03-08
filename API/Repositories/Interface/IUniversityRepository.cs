using API.Models;

namespace API_CodeFirst.Repositories.Interface;

public interface IUniversityRepository : IGeneralRepository<University, int>
{
    Task<IEnumerable<University>> GetByName(string name);
}

