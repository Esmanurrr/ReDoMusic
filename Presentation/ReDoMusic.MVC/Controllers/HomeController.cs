using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using ReDoMusic.Core.Domain.Entities;
using ReDoMusic.Core.Domain.Enums;
using ReDoMusic.Domain.Entities;
using ReDoMusic.Infrastructure.Persistence.Contexts;
using ReDoMusic.MVC.Models;
using System.Linq;

namespace ReDoMusic.MVC.Controllers
{
	public class HomeController : Controller
	{


		private readonly ReDoMusicDbContext _dbContext;

		public HomeController()
		{
			_dbContext = new();
		}

		[HttpGet]
		public IActionResult Index()
		{
			var categoriesWithColors = _dbContext.Categories.ToList();

			var categoryCheckboxes = categoriesWithColors
				.Select(i => new CheckboxViewModel
				{
					Id = i.Id,
					IsSelected = false,
					Name = i.Name,
				})
				.Distinct()
				.ToList();

			var instruments = _dbContext.Instruments.OrderByDescending(x=>x.Price).ToList();

			var viewModel = new CategoryColorViewModel
			{
				Categories = categoryCheckboxes,
				Instruments = instruments
			};

			viewModel.Colors = Enum.GetValues(typeof(Color)).Cast<Color>().Select(x => new ColorViewModel(x.ToString(), (int)x)).ToList();

			viewModel.PriceIntervals = new List<PriceIntervalViewModel>
			{
				new PriceIntervalViewModel { MinPrice = 0, MaxPrice = 500, IsSelected = false },
				new PriceIntervalViewModel { MinPrice = 500, MaxPrice = 1000, IsSelected = false },
				new PriceIntervalViewModel { MinPrice = 1000, MaxPrice = 1500, IsSelected = false },
				new PriceIntervalViewModel { MinPrice = 1500, MaxPrice = 2000, IsSelected = false },
			 
			};


			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Index([FromForm] CategoryColorViewModel viewModel)
		{
			var selectedCategoryIds = viewModel.Categories
				 .Where(c => c.IsSelected)
				 .Select(c => c.Id)
				 .ToList();

			var selectedColorNumbers = viewModel.Colors
				.Where(c => c.IsSelected)
				.Select(c => c.Number)
				.ToList();

			var selectedPriceIntervals = viewModel.PriceIntervals
				.Where(p => p.IsSelected)
				.Select(p => new { MinPrice = p.MinPrice, MaxPrice = p.MaxPrice })
				.ToList();

			var instruments = _dbContext.Instruments
				.Include(x => x.Category)
				.ToList();

			var filteredInstruments = instruments
				.Where(x =>
					(!selectedCategoryIds.Any() || selectedCategoryIds.Contains(x.Category.Id)) &&
					(!selectedColorNumbers.Any() || selectedColorNumbers.Contains((int)x.Color)) &&
					(!selectedPriceIntervals.Any() || selectedPriceIntervals.Any(interval => x.Price >= interval.MinPrice && x.Price <= interval.MaxPrice)))
				.OrderBy(x=>x.Price)
				.ToList();

			viewModel.Instruments = filteredInstruments;

			return View(viewModel);
		}

		[HttpGet]
        public IActionResult Search(string searchTerm = "")
        {
			var categories = _dbContext.Categories.ToList();
            // Arama sorgusu ile enstrümanları filtrele
            var instruments = _dbContext.Instruments
				.Include(x=>x.Category)
                .Where(x => x.Name.Contains(searchTerm) || x.Category.Name.Contains(searchTerm))
                .OrderBy(x => x.Price)
                .ToList();

			var categoriesWithColors = _dbContext.Categories.ToList();

			var categoryCheckboxes = categoriesWithColors
				.Select(i => new CheckboxViewModel
				{
					Id = i.Id,
					IsSelected = false,
					Name = i.Name,
				})
				.Distinct()
				.ToList();

			var colors = Enum.GetValues(typeof(Color)).Cast<Color>().Select(x => new ColorViewModel(x.ToString(), (int)x)).ToList();

			var prices = new List<PriceIntervalViewModel>
			{
				new PriceIntervalViewModel { MinPrice = 0, MaxPrice = 500, IsSelected = false },
				new PriceIntervalViewModel { MinPrice = 500, MaxPrice = 1000, IsSelected = false },
				new PriceIntervalViewModel { MinPrice = 1000, MaxPrice = 1500, IsSelected = false },
				new PriceIntervalViewModel { MinPrice = 1500, MaxPrice = 2000, IsSelected = false },

			};

			// CategoryColorViewModel içinde bu sorgu sonuçlarını kullanarak modeli doldurun
			var viewModel = new CategoryColorViewModel
			{
				Colors = colors,
				Categories = categoryCheckboxes,
				PriceIntervals = prices,
				Instruments = instruments,
            };

            // Sonucu Index view'ına gönderin
            return View("Index", viewModel);
        }


    }
}




