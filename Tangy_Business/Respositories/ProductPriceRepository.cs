using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tangy.Business.Respositories
{
    using Interface;
    using Data;
    using Data.Models;
    using Models;

    public class ProductPriceRepository : IProductPriceRepository
    {
        readonly AppDbContext _context;
        readonly IMapper _mapper;

        public ProductPriceRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductPriceDTO> Create(ProductPriceDTO productPrice)
        {
            var data = _mapper.Map<ProductPriceDTO, ProductPrice>(productPrice);
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductPrice, ProductPriceDTO>(data);
        }

        public async Task<int> Delete(int id)
        {
            var productPrice = await _context.ProductPrices.FirstOrDefaultAsync(f => f.Id == id);
            if (productPrice is not null)
            {
                _context.Remove(productPrice);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? Id=null)
        {
            if (Id is > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(
                    _context.ProductPrices
                    .Where(w => w.ProductId == Id));
            }
            return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(
                    _context.ProductPrices);
        }

        public async Task<ProductPriceDTO> GetById(int Id)
        {
            var productPrice = await _context.ProductPrices.FirstOrDefaultAsync(f => f.Id == Id);
            if (productPrice is not null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(productPrice);
            }
            return new ProductPriceDTO();
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO productPrice)
        {
            var data = await _context.ProductPrices.FirstOrDefaultAsync(f => f.Id == productPrice.Id);
            if (data is not null)
            {
                data.ProductId = productPrice.ProductId;
                data.Size = productPrice.Size;
                data.Price = productPrice.Price;
                _context.Update(data);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductPriceDTO>(data);
            }
            return productPrice;
        }
    }
}
