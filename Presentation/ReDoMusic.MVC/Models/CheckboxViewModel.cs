using ReDoMusic.Core.Domain.Entities;
using ReDoMusic.Core.Domain.Enums;
using ReDoMusic.Domain.Entities;

namespace ReDoMusic.MVC.Models
{
	public class CheckboxViewModel
	{
		public Guid Id { get; set; }
		public Category Category { get; set; }
		public Color Color { get; set; }
		public bool IsSelected { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}

	public class ColorViewModel
	{
		public ColorViewModel() { }
		public ColorViewModel(string name, int number)
		{
			Name = name;

			Number = number;
		}

		public string Name { get; set; }
		public int Number { get; set; }
		public bool IsSelected { get; set; }
	}

	public class PriceIntervalViewModel
	{
		public decimal MinPrice { get; set; }
		public decimal MaxPrice { get; set; }
		public bool IsSelected { get; set; }
	}

	public class CategoryColorViewModel
	{
		public List<PriceIntervalViewModel> PriceIntervals { get; set; }
		public List<CheckboxViewModel> Categories { get; set; }
		public List<ColorViewModel> Colors { get; set; }
		public List<Category>? FilteredCategories { get; set; }
		public List<Instrument> Instruments { get; set; }

	}
}


