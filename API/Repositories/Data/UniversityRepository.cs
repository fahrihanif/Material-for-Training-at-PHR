using API.Contexts;
using API.Models;
using API_CodeFirst.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API_CodeFirst.Repositories.Data;

public class UniversityRepository : GeneralRepository<University, int>, IUniversityRepository
{
    private readonly MyContext context;

    public UniversityRepository(MyContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<University>> GetByName(string name)
    {
        return await context.Universities.Where(u => u.Name == name).ToListAsync();
    }
}
