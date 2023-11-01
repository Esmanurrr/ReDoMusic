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
                .Select(i => new CheckboxViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    IsSelected = false
                })
                .Distinct()
                .ToList();

            var colorCheckboxes = instrumentsWithColors
                .Select(i => new CheckboxViewModel() { Name = i.Color.ToString(), Id = i.Id, IsSelected = false })
                .Distinct()
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


