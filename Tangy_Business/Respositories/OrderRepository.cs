using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy.Business.Respositories.Interface;
using Tangy.Common;
using Tangy.Data;
using Tangy.Data.Models;
using Tangy.Data.ViewModels;
using Tangy.Models;

namespace Tangy.Business.Respositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly AppDbContext _context;
        readonly IMapper _mapper;

        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Create(OrderDTO order)
        {
            try
            {
                var data = _mapper.Map<Order>(order);
                await _context.OrderHeaders.AddAsync(data.OrderHeader);
                await _context.SaveChangesAsync();

                foreach (var detail in data.OrderDetails)
                {
                    detail.OrderHeaderId = data.OrderHeader.Id;
                }

                await _context.OrderDetails.AddRangeAsync(data.OrderDetails);
                await _context.SaveChangesAsync();

                return new OrderDTO
                {
                    OrderHeader = _mapper.Map<OrderHeaderDTO>(data.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetailDTO>>(data.OrderDetails).ToList()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return order;
        }

        public async Task<int> Delete(int id)
        {
            var order = await _context.OrderHeaders.FirstOrDefaultAsync(f => f.Id == id);
            if (order is not null)
            {
                var detail = _context.OrderDetails.Where(w => w.OrderHeaderId == id);
                _context.OrderDetails.RemoveRange(detail);
                _context.OrderHeaders.Remove(order);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {
            var orders = new List<OrderDTO>();
            var orderHeaderList = _context.OrderHeaders;
            var orderDetailList = _context.OrderDetails;
            foreach (var header in orderHeaderList)
            {
                OrderDTO order = new OrderDTO
                {
                    OrderHeader = _mapper.Map<OrderHeaderDTO>(header),
                    OrderDetails = (ICollection<OrderDetailDTO>)_mapper.Map<OrderDetailDTO>(orderDetailList.Where(w => w.OrderHeaderId == header.Id))
                };
                orders.Add(order);
            }
            return orders;
        }

        public async Task<OrderDTO> GetById(int id)
        {
            var order = new Order
            {
                OrderHeader = await _context.OrderHeaders.FirstOrDefaultAsync(f => f.Id == id),
                OrderDetails = await _context.OrderDetails.Where(w => w.OrderHeaderId == id).ToListAsync()
            };

            if (order is not null)
                return _mapper.Map<OrderDTO>(order);

            return new OrderDTO();
        }

        public async Task<OrderHeaderDTO> MarkPaymentAsSuccessful(int id)
        {
            var header = await _context.OrderHeaders.FindAsync(id);
            if (header is null)
                return new OrderHeaderDTO();

            if (header.Status is Constants.Status.Pending)
            {
                header.Status = Constants.Status.Confirmed;
                await _context.SaveChangesAsync();
                return _mapper.Map<OrderHeaderDTO>(header);
            }
            
            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> UpdateOrderHeader(OrderHeaderDTO orderHeader)
        {
            if(orderHeader is not null)
            {
                var header = _mapper.Map<OrderHeader>(orderHeader);
                _context.OrderHeaders.Update(header);
                await _context.SaveChangesAsync();
                return orderHeader;
            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int id, string status)
        {
            var order = await _context.OrderHeaders.FindAsync(id);
            if (order is null)
                return false;

            if (order.Status is Constants.Status.Shipped)
                order.ShippingDate = DateTime.Now;

            order.Status = status;

            _context.OrderHeaders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
