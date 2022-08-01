using Tangy.Models;

namespace TangyWeb.Client.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);
    }
}
