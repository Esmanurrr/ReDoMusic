using ReDoMusic.Core.Domain.Common;
using ReDoMusic.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Domain.Entities
{
    public class Category : EntityBase<Guid>
    {
        public string Name { get; set; }

       public List<Instrument> Instruments { get; set; }
    }
}
