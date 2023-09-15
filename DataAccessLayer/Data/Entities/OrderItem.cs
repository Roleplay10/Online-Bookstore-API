﻿namespace DataAccessLayer.Data.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }


        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}