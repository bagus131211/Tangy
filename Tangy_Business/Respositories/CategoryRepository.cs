using AutoMapper;
using Tangy.Business.Respositories.Interface;
using Tangy.Data;
using Tangy.Data.Models;
using Tangy.Models;

namespace Tangy.Business.Respositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly AppDbContext _context;
        readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CategoryDTO Create(CategoryDTO category)
        {
            var data = _mapper.Map<CategoryDTO, Category>(category);
            data.CreatedDate = DateTime.Now;
            _context.Add(data);
            _context.SaveChanges();

            return _mapper.Map<Category, CategoryDTO>(data);
        }

        public int Delete(int Id)
        {
            var category = _context.Categories.FirstOrDefault(f => f.Id == Id);
            if (category is not null)
            {
                _context.Categories.Remove(category);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_context.Categories);
        }

        public CategoryDTO GetById(int Id)
        {
            var category = _context.Categories.FirstOrDefault(f => f.Id == Id);
            if (category is not null)
            {
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return new CategoryDTO();
        }

        public CategoryDTO Update(CategoryDTO category)
        {
            var data = _context.Categories.FirstOrDefault(f => f.Id == category.Id);
            if (data is not null)
            {
                data.Name = category.Name;
                _context.Categories.Update(data);
                _context.SaveChanges();
                return _mapper.Map<CategoryDTO>(data);
            }
            return category;
        }
    }
}
