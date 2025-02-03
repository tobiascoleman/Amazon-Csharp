namespace eCommerce.Models
{
    public class Product {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
		public decimal Price { get; set; }

        public string? Display {
            get {
                return $"{Id}. Product: {Name} Price: ${Price} Stock: {Quantity}";
            }
        }

        public Product() {
            Name = string.Empty;
        }

        public override string ToString() {
            return Display ?? string.Empty;
        }
    }
}