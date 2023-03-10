using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories.Data
{
    public class EducationRepository : GeneralRepository<Education,int> , IEducationRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;

        public EducationRepository(string request = "Educations/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7261/api/")
            };
            this.request = request;
        }

    }
}
