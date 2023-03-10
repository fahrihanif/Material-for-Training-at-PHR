using Client.Models;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Controllers
{
    [Authorize]
    public class EducationController : Controller
    {
        private readonly IEducationRepository repository;
        private readonly IUniversityRepository universityRepository;

        public EducationController(IEducationRepository repository, IUniversityRepository universityRepository)
        {
            this.repository = repository;
            this.universityRepository = universityRepository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var educations = new List<Education>();
            if (result.Data?.Any() is null)
            {
                return View(educations);
            }
            else
            {
                educations = result.Data?.Select(e => new Education
                {
                    Id = e.Id,
                    Major = e.Major,
                    GPA = e.GPA,
                    Degree = e.Degree,
                    UniversityId = e.UniversityId
                   
                }).ToList();
            }
            return View(educations);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var resultUniv = await universityRepository.Get();
            var universities = resultUniv.Data?.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name,
            }).ToList();

            ViewBag.University = universities;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Education education)
        {
            education.Id = 0;
            if (ModelState.IsValid)
            {
                var result = await repository.Post(education);
                if (result.StatusCode == "200")
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == "409")
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }
            return View();
        }


    }
}
