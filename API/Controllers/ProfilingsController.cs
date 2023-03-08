using API.Models;
using API_CodeFirst.Base;
using API_CodeFirst.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_CodeFirst.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProfilingsController : BaseController<IProfilingRepository, Profiling, int>
{
    public ProfilingsController(IProfilingRepository repository) : base(repository)
    {
    }
}
