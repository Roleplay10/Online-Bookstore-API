﻿namespace Online_Bookstore_API.Data.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}