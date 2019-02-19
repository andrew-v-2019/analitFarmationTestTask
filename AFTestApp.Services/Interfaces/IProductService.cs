using System.Collections.Generic;
using System.Threading.Tasks;
using AFTestApp.ViewModels;

namespace AFTestApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetProducts();
    }
}
