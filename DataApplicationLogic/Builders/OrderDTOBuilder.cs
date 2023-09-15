using DataAccessLayer.Data.Entities;
using DataBusinessLogic.DTOs.OrderDTOs;

namespace DataBusinessLogic.Builders
{
    public interface IOrderDTOBuilder
    {
        List<OrderItem> createItemList(int order_id, List<OrderQuantityDTO> qty);
        Order CreateOrder(int user_id);
        OrderInfoDTO CreateOrderInfo(Order order);
        OrderItemInfoDTO CreateOrderItemInfo(OrderItem orderItem);
    }
    public class OrderDTOBuilder : IOrderDTOBuilder
    {
        public List<OrderItem> createItemList(int order_id, List<OrderQuantityDTO> qty)
        {
            List<OrderItem> list = qty.Select(item => new OrderItem
            {
                OrderId = order_id,
                BookId = item.BookId,
                Quantity = item.Qty,
                Subtotal = item.Price * item.Qty
            }).ToList();
            return list;
        }
        public Order CreateOrder(int user_id)
        {
            var order = new Order
            {
                UserId = user_id,
                OrderDate = DateTime.Now,
                Status = "pending",
            };
            return order;
        }
        public OrderInfoDTO CreateOrderInfo(Order order)
        {
            return new OrderInfoDTO
            {
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = order.OrderItems.Select(item => CreateOrderItemInfo(item)).ToList()
            };
        }
        public OrderItemInfoDTO CreateOrderItemInfo(OrderItem orderItem)
        {
            return new OrderItemInfoDTO
            {
                BookId = orderItem.BookId,
                Quantity = orderItem.Quantity,
                Subtotal = orderItem.Subtotal
            };
        }
    }
}
