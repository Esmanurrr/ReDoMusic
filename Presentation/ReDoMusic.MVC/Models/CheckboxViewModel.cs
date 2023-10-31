using ReDoMusic.Core.Domain.Entities;
using ReDoMusic.Core.Domain.Enums;

namespace ReDoMusic.MVC.Models
{
    public class CheckboxViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }


    public class InstrumentColorViewModel
    {
        public List<CheckboxViewModel> Instruments { get; set; }
        public List<CheckboxViewModel> Colors { get; set; }
        public List<Instrument> FilteredInstruments { get; set; }
        public List<Color> FilteredColors { get; set; } 
    }
}
