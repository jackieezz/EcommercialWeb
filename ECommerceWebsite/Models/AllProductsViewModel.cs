using Shared;

namespace ECommerceWebsite.Models
{
    public class AllProductsViewModel : ViewModelBase
    {
        public IEnumerable<ProductDTO>? products { get; set; }
        public int countProduct { get; set; }
    }
}
