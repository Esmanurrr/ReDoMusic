using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using ReDoMusic.Core.Domain.Entities;
using ReDoMusic.Core.Domain.Enums;
using ReDoMusic.Infrastructure.Persistence.Contexts;
using ReDoMusic.MVC.Models;

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

			var colorCheckboxes = _dbContext.Instruments.ToList()
				.Select(i => new CheckboxViewModel() { Color = i.Color, Id = i.Id, IsSelected = false })
				.Distinct()
				.ToList();

			var instruments = _dbContext.Instruments.ToList();

			var viewModel = new CategoryColorViewModel
			{
				Categories = categoryCheckboxes,
				Colors = colorCheckboxes,
				Instruments = instruments
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Index([FromForm] CategoryColorViewModel viewModel)
		{
			List<Core.Domain.Entities.Instrument> instruments = new();
			foreach(var category in viewModel.Categories)
			{
				var instrument = _dbContext.Instruments.Where(x => x.Category.Id == category.Id && category.IsSelected).ToList();
				instruments.AddRange(instrument);
			}
			List<Color> colors = new();
			foreach (var color in viewModel.Colors)
			{
				//Enum.GetValues(typeof(Color))
				var colorList = _dbContext.Instruments.Where(x => x.Color== color.Color && color.IsSelected).ToList();
				//colors.AddRange(colorList);
			}



			viewModel.Instruments = instruments;
			//viewModel.Colors = colors;

			return View(viewModel);
		}
	}
}


