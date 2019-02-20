using System.Collections.Generic;
using AFTestApp.DtoModels;

namespace AFTestApp.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
    }
}
