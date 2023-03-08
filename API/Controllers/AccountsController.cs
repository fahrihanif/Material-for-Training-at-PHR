using API.Models;
using API_CodeFirst.Base;
using API_CodeFirst.Repositories.Interface;
using API_CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API_CodeFirst.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : BaseController<IAccountRepository, Account, string>
{
    private readonly IAccountRepository repository;

    public AccountsController(IAccountRepository repository) : base(repository)
    {
        this.repository = repository;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterVM registerVM)
    {
        var result = await repository.Register(registerVM);

        if (result is 0)
        {
            return Conflict(new
            {
                StatusCode = 409,
                Message = "Failed To Register!"
            });
        }
        return Ok(new
        {
            StatusCode = 200,
            Message = "Successfully Registered!"
        });
    }
}
