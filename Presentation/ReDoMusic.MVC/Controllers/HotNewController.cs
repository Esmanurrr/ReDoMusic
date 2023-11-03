using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoMusic.Infrastructure.Persistence.Contexts;
using ReDoMusic.MVC.Models;
using ReDoMusic.Core.Domain.Entities;

namespace ReDoMusic.MVC.Controllers
{
	public class HotNewController : Controller
	{

		private readonly ReDoMusicDbContext _dbContext;

		public HotNewController()
		{
			_dbContext = new();
		}

		public IActionResult Index()
		{
            //var category = _dbContext.Categories
            //             .Include(x => x.Instruments)
            //             .OrderByDescending(x => x.CreatedOn)
            //             .Take(3)
            //             .ToList();

            //         var instrument = _dbContext.Instruments
            //	.Include(x=>x.Category)
            //	.OrderByDescending(x => x.CreatedOn)
            //	.Take(3)
            //	.ToList();

            //var viewModel = new CategoryColorViewModel()
            //{
            //	FilteredCategories = category,
            //	Instruments = instrument,
            //};

            //return View(viewModel);

            var categories = _dbContext.Categories
            .Include(x => x.Instruments)
            .ToList();

            var hotInstruments = new List<ReDoMusic.Core.Domain.Entities.Instrument>();

            foreach (var category in categories)
            {
                var instruments = category.Instruments
                    .OrderByDescending(x => x.CreatedOn)
                    .Take(3)
                    .OrderByDescending(x=>x.Price) 
                    .ToList();

                hotInstruments.AddRange(instruments);
            }

            var viewModel = new CategoryColorViewModel()
            {
                FilteredCategories = categories,
                Instruments = hotInstruments,
            };

            return View(viewModel);
        }
	}
}
