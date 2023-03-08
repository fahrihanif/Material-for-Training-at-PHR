using API.Models;
using API_CodeFirst.Base;
using API_CodeFirst.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_CodeFirst.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController<IRoleRepository, Role, int>
{
    public RolesController(IRoleRepository repository) : base(repository)
    {
    }
}
