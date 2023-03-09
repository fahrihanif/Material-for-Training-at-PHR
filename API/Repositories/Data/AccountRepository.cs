using API.Contexts;
using API.Models;
using API_CodeFirst.Handlers;
using API_CodeFirst.Repositories.Interface;
using API_CodeFirst.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_CodeFirst.Repositories.Data;

public class AccountRepository : GeneralRepository<Account, string>, IAccountRepository
{
    private readonly MyContext context;

    public AccountRepository(MyContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<UserDataVM> GetUserData(string email)
    {
        var result = await context.Employees.Select(e => new UserDataVM
        {
            Email = e.Email,
            FullName = String.Concat(e.FirstName, " ", e.LastName),
        }).FirstOrDefaultAsync(e => e.Email == email);

        return result;
    }

    public async Task<bool> Login(LoginVM loginVM)
    {
        /*var userdataMethod = await context.Employees.Join(context.Accounts, e => e.NIK, a => a.EmployeeNIK, (e, a) => new LoginVM
        {
            Email = e.Email,
            Passsword = a.Password
        }).FirstOrDefaultAsync(e => e.Email == loginVM.Email);*/

        var userdataQuery = await (from e in context.Employees
                                   join a in context.Accounts
                                   on e.NIK equals a.EmployeeNIK
                                   select new LoginVM
                                   {
                                       Email = e.Email,
                                       Passsword = a.Password
                                   }).FirstOrDefaultAsync(e => e.Email == loginVM.Email);

        if (userdataQuery is null)
        {
            return false;
        }

        return Hashing.ValidatePassword(loginVM.Passsword, userdataQuery.Passsword);
    }

    public async Task<int> Register(RegisterVM registerVM)
    {
        using (var transaction = await context.Database.BeginTransactionAsync())
        {
            try
            {
                var university = new University()
                {
                    Name = registerVM.UniversityName
                };
                await context.Universities.AddAsync(university);
                await context.SaveChangesAsync();

                var education = new Education()
                {
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    GPA = registerVM.GPA,
                    UniversityId = university.Id
                };

                await context.Educations.AddAsync(education);
                await context.SaveChangesAsync();

                var employee = new Employee()
                {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    HiringDate = registerVM.HiringDate,
                    PhoneNumber = registerVM.PhoneNumber,
                    Email = registerVM.Email,
                    IsActive = true
                };

                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();

                var profiling = new Profiling()
                {
                    EmployeeNIK = registerVM.NIK,
                    EducationId = education.Id,
                };

                await context.Profilings.AddAsync(profiling);
                await context.SaveChangesAsync();

                var account = new Account()
                {
                    EmployeeNIK = registerVM.NIK,
                    Password = Hashing.HashPassword(registerVM.Password),
                };

                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
                return 1;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return 0;
            }
        }


    }
}
