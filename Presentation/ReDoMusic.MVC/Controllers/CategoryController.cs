using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoMusic.Domain.Entities;
using ReDoMusic.Infrastructure.Persistence.Contexts;

namespace ReDoMusic.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;

        public CategoryController()
        {
            _dbContext = new();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            var category = new Category() { Name = name, Id = Guid.NewGuid()};

            _dbContext.Categories.Add(category);

            _dbContext.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var category = _dbContext.Categories.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(string id, string name)
        {
            var category = _dbContext.Categories.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            category.Name = name;

            _dbContext.SaveChanges();

            return RedirectToAction("index");

        }

        [Route("[controller]/[action]/{id}")]
        public IActionResult Delete(string id)
        {
            var category = _dbContext.Categories.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
