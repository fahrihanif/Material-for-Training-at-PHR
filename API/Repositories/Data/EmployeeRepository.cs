using API.Contexts;
using API.Models;
using API_CodeFirst.Repositories.Interface;

namespace API_CodeFirst.Repositories.Data;

public class EmployeeRepository : GeneralRepository<Employee, string>, IEmployeeRepository
{
    public EmployeeRepository(MyContext context) : base(context)
    {
    }
}
