namespace Tangy.Business.Respositories.Interface
{
    using Models;

    public interface IProductPriceRepository
    {
        Task<ProductPriceDTO> Create(ProductPriceDTO category);
        Task<ProductPriceDTO> Update(ProductPriceDTO category);
        Task<int> Delete(int Id);
        Task<ProductPriceDTO> GetById(int Id);
        Task<IEnumerable<ProductPriceDTO>> GetAll(int? Id=null);
    }
}
