using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tangy.Business.Respositories
{
    using Interface;
    using Data;
    using Data.Models;
    using Models;

    public class ProductRepository : IProductRepository
    {
        readonly AppDbContext _context;
        readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO product)
        {
            var data = _mapper.Map<ProductDTO, Product>(product);
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(data);
        }

        public async Task<int> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(f => f.Id == id);
            if (product is not null)
            {
                _context.Remove(product);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_context.Products.Include(i => i.Category));
        }

        public async Task<ProductDTO> GetById(int Id)
        {
            var product = await _context.Products.Include(i => i.Category).FirstOrDefaultAsync(f => f.Id == Id);
            if (product is not null)
            {
                return _mapper.Map<Product, ProductDTO>(product);
            }
            return new ProductDTO();
        }

        public async Task<ProductDTO> Update(ProductDTO product)
        {
            var data = await _context.Products.FirstOrDefaultAsync(f => f.Id == product.Id);
            if (data is not null)
            {
                data.Name = product.Name;
                data.Description = product.Description;
                data.Colour = product.Colour;
                data.CustomerFavourites = product.CustomerFavourites;
                data.ShopFavourites = product.ShopFavourites;
                data.CategoryId = product.CategoryId;
                _context.Update(data);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductDTO>(data);
            }
            return product;
        }
    }
}
