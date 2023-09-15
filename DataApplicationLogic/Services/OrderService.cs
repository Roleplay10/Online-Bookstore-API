using DataAccessLayer.Data;
using DataAccessLayer.Data.Entities;
using DataBusinessLogic.Builders;
using DataBusinessLogic.DTOs.OrderDTOs;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DataBusinessLogic.Services
{
    public interface IOrderService
    {
        Task<bool> PlaceOrder(int user_id, List<OrderQuantityDTO> qty);
        List<OrderInfoDTO> GetUserOrders(int user_id);
    }
    public class OrderService : IOrderService
    {
        private readonly BookStoreDb _db;
        private readonly IOrderDTOBuilder _orderDTOBuilder;
        public OrderService(BookStoreDb db, IOrderDTOBuilder orderDTOBuilder)
        {
            _db = db;
            _orderDTOBuilder = orderDTOBuilder;
        }
        public async Task<bool> PlaceOrder(int user_id, List<OrderQuantityDTO> qty)
        {
            var order = _orderDTOBuilder.CreateOrder(user_id);
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            var list = _orderDTOBuilder.createItemList(user_id, qty);
            double total = 0;
            list.ForEach(item =>
            {
                _db.OrderItems.Add(item);
                total += item.Subtotal;
            });
            order.TotalAmount = total;
            order.OrderItems = list;
            await _db.SaveChangesAsync();
            //var order = new Order();
            return true;
        }
        public List<OrderInfoDTO> GetUserOrders(int user_id)
        {
            if(user_id == 0)
            {
                return null;
            }
            var orders = _db.Orders
                .Where(u => u.UserId == user_id)
                .Include(u => u.OrderItems)
                .Select(order => _orderDTOBuilder.CreateOrderInfo(order))
                .ToList();
            return orders;
        }
    }
}
