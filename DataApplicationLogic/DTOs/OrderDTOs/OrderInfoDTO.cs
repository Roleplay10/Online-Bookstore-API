using DataAccessLayer.Data.Entities;

namespace DataBusinessLogic.DTOs.OrderDTOs
{
    public class OrderInfoDTO
    {
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemInfoDTO> OrderItems { get; set; }
    }
}
