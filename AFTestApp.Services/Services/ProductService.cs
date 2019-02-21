using System.Collections.Generic;
using System.Linq;
using AFTestApp.Data;
using AFTestApp.Services.Interfaces;
using AFTestApp.DtoModels;

namespace AFTestApp.Services.Services
{
    public class ProductService: IProductService
    {
        private readonly IAfTestAppContextFactory _afTestAppContextFactory;

        public ProductService(IAfTestAppContextFactory afTestAppContextFactory)
        {
            _afTestAppContextFactory = afTestAppContextFactory;
        }

        public List<ProductDto> GetProducts()
        {
            using (var context = _afTestAppContextFactory.CreateContext())
            {
                var products = context.Products.Select(x => new ProductDto
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
