using ECommerce.Entities.Models;

namespace ECommerce.WebUI.Models
{
    public class ProductListViewModel
    {
        public List<Product>? Products { get; set; }
        public string? FilterAz  { get; set; }
        public string? FilterHl  { get; set; }
        public int CategoryId {  get; set; }
    }
}