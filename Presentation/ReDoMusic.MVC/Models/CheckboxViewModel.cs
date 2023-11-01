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
    }


    public class CategoryColorViewModel
    {
        public List<CheckboxViewModel> Categories { get; set; }
        public List<CheckboxViewModel> Colors { get; set; }
        public List<Category>? FilteredCategories { get; set; }
        public List<Color> FilteredColors { get; set; } 
        public List<Instrument> Instruments { get; set; }
        
    }
}
