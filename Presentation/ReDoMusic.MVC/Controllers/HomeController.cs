using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var instrumentsWithColors = _dbContext.Instruments.ToList();

            var instrumentCheckboxes = instrumentsWithColors
                .Select(i => i.Name)
                .Distinct()
                .Select(instrumentType => new CheckboxViewModel
                {
                    Name = instrumentType.ToString(),
                    IsSelected = false
                })
                .ToList();

            var colorCheckboxes = instrumentsWithColors
                .Select(i => i.Color.ToString())
                .Distinct()
                .Select(colorString => new CheckboxViewModel
                {
                    Name = colorString,
                    IsSelected = false
                })
                .ToList();

            var viewModel = new InstrumentColorViewModel
            {
                Instruments = instrumentCheckboxes,
                Colors = colorCheckboxes
            };

            return View(viewModel);
        }

    }

}


