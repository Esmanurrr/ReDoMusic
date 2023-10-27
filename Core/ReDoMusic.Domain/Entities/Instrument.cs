using ReDoMusic.Core.Domain.Common;
using ReDoMusic.Core.Domain.Common;
using ReDoMusic.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Core.Domain.Entities
{
    public class Instrument : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Brand Brand { get; set; }
        public decimal Price { get; set; }
        public Color Color { get; set; }
        public string Barcode { get; set; }
        public string PictureUrl { get; set; }
    }
}
