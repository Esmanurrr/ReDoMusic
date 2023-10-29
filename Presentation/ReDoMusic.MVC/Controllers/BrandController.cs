using Microsoft.AspNetCore.Mvc;
using ReDoMusic.Core.Domain.Entities;
using ReDoMusic.Infrastructure.Persistence.Contexts;

namespace ReDoMusic.MVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;

        public BrandController()
        {
            _dbContext = new();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var brands = _dbContext.Brands.ToList();

            return View(brands);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string brandName, string brandDisplayingText, string brandAddress)
        {
            var brand = new Brand()
            {
                Name = brandName,
                DisplayingText = brandDisplayingText,
                Address = brandAddress,
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
            };
            //default değerler gelsin

            _dbContext.Brands.Add(brand);

            _dbContext.SaveChanges();

            return View();
        }

         [HttpGet]
         public IActionResult Update()
         {
             return View();
         } 

         [HttpPost]
        public IActionResult Update(string id,string brandName, string brandDisplayingText, string brandAddress)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            brand.Name= brandName;
            brand.DisplayingText= brandDisplayingText;
            brand.Address= brandAddress;

            _dbContext.SaveChanges();

            return RedirectToAction("index");

        }

        [Route("[controller]/[action]/{id}")]
        public IActionResult Delete(string id)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

            _dbContext.Brands.Remove(brand);

            _dbContext.SaveChanges();

            return RedirectToAction("index");
        }
    }

}
