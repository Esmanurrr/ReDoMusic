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


			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Index([FromForm] CategoryColorViewModel viewModel)
		{
			var selectedCategories = viewModel.Categories
				.Where(x => x.IsSelected)
				.Select(x => x.Id)
				.ToList();


			var selectedColors = viewModel.Colors
				.Where(x => x.IsSelected)
				.Select(x => x.Number)
				.ToList();

			bool isFiltered = viewModel.Categories.Where(x => x.IsSelected).Any() || viewModel.Colors.Where(x => x.IsSelected).Any();

			viewModel.Instruments = _dbContext.Instruments
				.Include(x => x.Category)
				.Where(x => !isFiltered || (selectedCategories.Contains(x.Category.Id) || selectedColors.Contains((int)x.Color)))
				.ToList();

			return View(viewModel);
		}
	}
}


