namespace eCommerce.Models
{
	public class ShoppingCart {
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string? Name { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total => Quantity * Price;
		public string? Display {
            get {
                return $"{Id}. {Quantity} {Name} = ${Total}";
            }
        }
		public ShoppingCart() {
            Name = string.Empty;
		}
		public override string ToString() {
			return Display ?? string.Empty;
		}
	}
}