using API.Models;
using API_CodeFirst.ViewModels;

namespace API_CodeFirst.Repositories.Interface;

public interface IAccountRepository : IGeneralRepository<Account, string>
{
    Task<int> Register(RegisterVM registerVM);
    Task<bool> Login(LoginVM loginVM);
    Task<UserDataVM> GetUserData(string email);
}
