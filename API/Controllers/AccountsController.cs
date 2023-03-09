using API.Models;
using API_CodeFirst.Base;
using API_CodeFirst.Repositories.Interface;
using API_CodeFirst.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_CodeFirst.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : BaseController<IAccountRepository, Account, string>
{
    private readonly IAccountRepository repository;
    private readonly IConfiguration configuration;

    public AccountsController(IAccountRepository repository, IConfiguration configuration) : base(repository)
    {
        this.repository = repository;
        this.configuration = configuration;
    }

    [HttpPost("Register")]
    [AllowAnonymous]
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

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(LoginVM loginVM)
    {
        var result = await repository.Login(loginVM);
        if (!result)
        {
            return NotFound(new
            {
                StatusCode = 404,
                Message = "Email Or Password is Not Found!"
            });
        }

        var userdata = await repository.GetUserData(loginVM.Email);
        var claims = new List<Claim>()
        {
            new Claim("Email", userdata.Email),
            new Claim("FullName", userdata.FullName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: signIn
            );

        var generateToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            StatusCode = 200,
            Message = "Login Success!",
            Data = generateToken
        });
    }
}
