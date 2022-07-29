namespace Tangy.Business.Respositories.Interface
{
    using Models;

    public interface IProductRepository
    {
        Task<ProductDTO> Create(ProductDTO category);
        Task<ProductDTO> Update(ProductDTO category);
        Task<int> Delete(int Id);
        Task<ProductDTO> GetById(int Id);
        Task<IEnumerable<ProductDTO>> GetAll();
    }
}
