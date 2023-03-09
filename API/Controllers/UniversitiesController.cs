using API.Models;
using API_CodeFirst.Base;
using API_CodeFirst.Handlers;
using API_CodeFirst.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_CodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = $"{UserRoles.ADMIN}")]
public class UniversitiesController : BaseController<IUniversityRepository, University, int>
{

    public UniversitiesController(IUniversityRepository repository) : base(repository)
    {
    }
}
