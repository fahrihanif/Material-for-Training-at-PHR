using Client.Models;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    //[Authorize]
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository repository;

        public UniversityController(IUniversityRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var universities = new List<University>();

            if (result.Data != null) 
            {
                universities = result.Data?.Select(e => new University
                {
                    Id = e.Id,
                    Name = e.Name,
                }).ToList();
            }

            return View(universities);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(University university)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Post(university);
                if (result.StatusCode == "200")
                {
                    return RedirectToAction(nameof(Index));
                } else if (result.StatusCode == "409")
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(University university)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Put(university.Id,university);
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await repository.Get(id);
            var university = new University();
            if (result.Data?.Id is null)
            {
                return View(university);
            }
            else
            {
                university.Id = result.Data.Id;
                university.Name = result.Data.Name; 
            }

            return View(university);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await repository.Get(id);
            var university = new University();
            if (result.Data?.Id is null)
            {
                return View(university);
            }
            else
            {
                university.Id = result.Data.Id;
                university.Name = result.Data.Name;
            }
            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await repository.Delete(id);
            if (result.StatusCode == "200")
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


    }
}
