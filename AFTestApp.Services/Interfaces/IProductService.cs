using System.Collections.Generic;
using AFTestApp.ViewModels;

namespace AFTestApp.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductViewModel> GetProducts();
    }
}
