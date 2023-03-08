using API.Contexts;
using API.Models;
using API_CodeFirst.Repositories.Interface;

namespace API_CodeFirst.Repositories.Data;

public class EducationRepository : GeneralRepository<Education, int>, IEducationRepository
{
    public EducationRepository(MyContext context) : base(context)
    {
    }
}
