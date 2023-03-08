using API.Contexts;
using API.Models;
using API_CodeFirst.Repositories.Interface;

namespace API_CodeFirst.Repositories.Data;

public class RoleRepository : GeneralRepository<Role, int>, IRoleRepository
{
    public RoleRepository(MyContext context) : base(context)
    {
    }
}
