using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AFTestApp.Data;
using AFTestApp.ViewModels;
using AFTestApp.Services.Interfaces;

namespace AFTestApp.Services.Services
{
    public class ProductService: IProductService
    {
        private readonly IAfTestAppContextFactory _afTestAppContextFactory;

        public ProductService(IAfTestAppContextFactory afTestAppContextFactory)
        {
            _afTestAppContextFactory = afTestAppContextFactory;
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                var products = await context.Products.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    Code = x.Code,
                    ProductId = x.ProductId,
                    Price = x.Price
                }).ToListAsync();
                return products;
            }
        }
    }
}
