using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CategoryDTO> Create(CategoryDTO category)
        {
            var data = _mapper.Map<CategoryDTO, Category>(category);
            data.CreatedDate = DateTime.Now;
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDTO>(data);
        }

        public async Task<int> Delete(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(f => f.Id == Id);
            if (category is not null)
            {
                _context.Categories.Remove(category);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_context.Categories);
        }

        public async Task<CategoryDTO> GetById(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(f => f.Id == Id);
            if (category is not null)
            {
                return _mapper.Map<Category, CategoryDTO>(category);
            }
            return new CategoryDTO();
        }

        public async Task<CategoryDTO> Update(CategoryDTO category)
        {
            var data = await _context.Categories.FirstOrDefaultAsync(f => f.Id == category.Id);
            if (data is not null)
            {
                data.Name = category.Name;
                _context.Categories.Update(data);
                _context.SaveChangesAsync();
                return _mapper.Map<CategoryDTO>(data);
            }
            return category;
        }
    }
}
