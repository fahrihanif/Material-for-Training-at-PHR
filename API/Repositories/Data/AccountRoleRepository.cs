using API.Contexts;
using API.Models;
using API_CodeFirst.Repositories.Interface;

namespace API_CodeFirst.Repositories.Data;

public class AccountRoleRepository : GeneralRepository<AccountRole, int>, IAccountRoleRepository
{
    public AccountRoleRepository(MyContext context) : base(context)
    {
    }
}
