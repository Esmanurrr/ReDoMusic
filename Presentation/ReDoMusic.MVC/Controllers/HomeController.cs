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

			var instruments = _dbContext.Instruments.ToList();

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
				.ToList();

			viewModel.Instruments = filteredInstruments;

			return View(viewModel);
		}

	}
}




