using Tangy.Models;

namespace Tangy.Business.Respositories.Interface
{
    public interface ICategoryRepository
    {
        Task<CategoryDTO> Create(CategoryDTO category);
        Task<CategoryDTO> Update(CategoryDTO category);
        Task<int> Delete(int Id);
        Task<CategoryDTO> GetById(int Id);
        Task<IEnumerable<CategoryDTO>> GetAll();
    }
}
