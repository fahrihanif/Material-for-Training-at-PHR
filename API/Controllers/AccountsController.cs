using API.Models;
using API_CodeFirst.Base;
using API_CodeFirst.Handlers;
using API_CodeFirst.Repositories.Interface;
using API_CodeFirst.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_CodeFirst.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : BaseController<IAccountRepository, Account, string>
{
    private readonly IAccountRepository repository;
    private readonly ITokenService _tokenService;

    public AccountsController(IAccountRepository repository, ITokenService tokenService) : base(repository)
    {
        this.repository = repository;
        _tokenService = tokenService;
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
            new Claim(ClaimTypes.Email, userdata.Email),
            new Claim(ClaimTypes.Name, userdata.Email),
            new Claim(ClaimTypes.NameIdentifier, userdata.FullName)
        };

        var getRoles = await repository.GetRoles(loginVM.Email);
        foreach (var item in getRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, item));
        }

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await repository.UpdateToken(userdata.Email, refreshToken, DateTime.Now.AddDays(1)); // Token will expired in a day

        var generateToken = new AuthenticatedResponseVM
        {
            Token = accessToken,
            RefreshToken = refreshToken
        };

        return Ok(new
        {
            StatusCode = 200,
            Message = "Login Success!",
            Data = generateToken
        });
    }

    [HttpPost]
    [Route("RefreshToken")]
    public async Task<IActionResult> Refresh(TokenAPIVM tokenApi)
    {
        if (tokenApi is null)
            return BadRequest("Invalid client request");

        var accessToken = tokenApi.AccessToken;
        var refreshToken = tokenApi.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        var email = principal.Identity.Name; //this is mapped to the Name claim by default

        var user = await repository.GetAccountByEmail(email);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid client request");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        repository.UpdateToken(email, newRefreshToken);

        var refreshedToken = new AuthenticatedResponseVM
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        };

        return Ok(new
        {
            StatusCode = 200,
            Message = "New Token Generated!",
            Data = refreshedToken
        });
    }

    [HttpPost, Authorize]
    [Route("RevokeToken")]
    public async Task<IActionResult> Revoke()
    {
        var email = User.Identity.Name;

        var user = await repository.GetAccountByEmail(email);
        if (user is null) return BadRequest();

        await repository.UpdateToken(email, null);

        return NoContent();
    }
}
