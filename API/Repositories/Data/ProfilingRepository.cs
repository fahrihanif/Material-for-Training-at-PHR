using API.Contexts;
using API.Models;
using API_CodeFirst.Repositories.Interface;

namespace API_CodeFirst.Repositories.Data;

public class ProfilingRepository : GeneralRepository<Profiling, int>, IProfilingRepository
{
    public ProfilingRepository(MyContext context) : base(context)
    {
    }
}
