using API.Models;
using API_CodeFirst.ViewModels;

namespace API_CodeFirst.Repositories.Interface;

public interface IAccountRepository : IGeneralRepository<Account, string>
{
    Task<int> Register(RegisterVM registerVM);
    Task<bool> Login(LoginVM loginVM);
    Task<UserDataVM> GetUserData(string email);
    Task<List<string>> GetRoles(string email);
    Task<int> UpdateToken(string email, string refreshToken, DateTime expiryTime);
    Task<int> UpdateToken(string email, string refreshToken);
    Task<Account?> GetAccountByEmail(string email);
}
