using Tangy.Models;

namespace Tangy.Business.Respositories.Interface
{
    public interface ICategoryRepository
    {
        CategoryDTO Create(CategoryDTO category);
        CategoryDTO Update(CategoryDTO category);
        int Delete(int Id);
        CategoryDTO GetById(int Id);
        IEnumerable<CategoryDTO> GetAll();
    }
}
