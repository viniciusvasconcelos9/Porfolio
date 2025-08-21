namespace EcommerceAPI.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        // 🔹 Relação: Um cliente pode ter vários pedidos
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
