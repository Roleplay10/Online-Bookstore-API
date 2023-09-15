namespace DataAccessLayer.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
        public ICollection<Order> Orders { get; set; }
        
        public User()
        {
            ShoppingCart = new ShoppingCart();
            Orders = new List<Order>();
        }
    }
}
