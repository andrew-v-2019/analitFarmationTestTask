using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public List<ProductViewModel> GetProducts()
        {
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                var products = context.Products.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    Code = x.Code,
                    ProductId = x.ProductId,
                    Price = x.Price
                }).ToList();
                return products;
            }
        }
    }
}
