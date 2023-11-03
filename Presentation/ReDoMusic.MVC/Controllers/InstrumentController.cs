using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoMusic.Core.Domain.Entities;
using ReDoMusic.Core.Domain.Enums;
using ReDoMusic.Domain.Entities;
using ReDoMusic.Infrastructure.Persistence.Contexts;
using ReDoMusic.MVC.Models;

namespace ReDoMusic.MVC.Controllers.Instrument
{
    public class InstrumentController : Controller
    {

        private readonly ReDoMusicDbContext _dbContext;

        public InstrumentController()
        {
            _dbContext = new();
        }

        public IActionResult Index()   //Anasayfada listelemek için . Index.cshtml view card yapısıyla gösterilecek.
        {
            var instruments = _dbContext.Instruments.ToList();
            return View(instruments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var brands = _dbContext.Brands.ToList();

            var categories = _dbContext.Categories.ToList();

            var model = new BrandCategoryModel()
            {
                Brands = brands,
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(string name, string description, string brandId, decimal price, Color color, string barcode, string pictureUrl, string categoryId)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(brandId)).FirstOrDefault();
            var category = _dbContext.Categories.Where(x => x.Id == Guid.Parse(categoryId)).FirstOrDefault();

            var instrument = new Core.Domain.Entities.Instrument()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Price = price,
                Color = color,
                Barcode = barcode,
                PictureUrl = pictureUrl,
                Brand = brand,
                Category = category
                
            };

            _dbContext.Instruments.Add(instrument);
            _dbContext.SaveChanges();

            return RedirectToAction("Add");
        }

        [HttpGet]
        [Route("Intstrument/Details/{id}")]
        public IActionResult Details(string id)
        {
			var instrument = _dbContext.Instruments.FirstOrDefault(x => x.Id == Guid.Parse(id));

			return View(instrument);
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Guid id, string name, string description, string brandId, decimal price, Color color, string barcode, string pictureUrl, Category categoryId) 
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(brandId)).FirstOrDefault();
            var instrument = _dbContext.Instruments.FirstOrDefault(x => x.Id ==id);

            if(instrument is not null) 
            {
                instrument.Name = name;
                instrument.Description = description;
                instrument.Brand = brand;
                instrument.Price = price;
                instrument.Color = color;
                instrument.Barcode = barcode;
                instrument.PictureUrl = pictureUrl;
                instrument.Category = categoryId;

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}
