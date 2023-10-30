using ReDoMusic.Core.Domain.Entities;
using ReDoMusic.Domain.Entities;

namespace ReDoMusic.MVC.Models
{
    public class BrandCategoryModel
    {
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }
    }
}
