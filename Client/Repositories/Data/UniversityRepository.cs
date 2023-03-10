using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;

namespace Client.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<University,int> , IUniversityRepository
    {
     

        public UniversityRepository(string request = "Universities/") : base(request)
        {

        }



    }
}
