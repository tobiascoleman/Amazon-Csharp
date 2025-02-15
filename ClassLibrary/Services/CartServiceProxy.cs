using eCommerce.Models;

namespace Library.eCommerce.Services
{
	public class CartServiceProxy {
		private CartServiceProxy() {
			CartItems = new List<ShoppingCart?>();
		}
		private int LastKey {
			get
			{
				if (!CartItems.Any())
				{
					return 0;
				}
				return CartItems.Select(p => p?.Id ?? 0).Max();
			}
		}

        private static CartServiceProxy? instance;
        private static object instanceLock = new object();
        public static CartServiceProxy Current {
            get
            {
                lock(instanceLock)
                {
                    instance ??= new CartServiceProxy();
                }

                return instance;
            }
        }

		public ShoppingCart? AddOrUpdate(ShoppingCart ShoppingCart) {
			var product = ProductServiceProxy.Current.Inventory.FirstOrDefault(p => p?.Id == ShoppingCart.ProductId);
			if (product == null) {
				Console.WriteLine("Product not found.");
				return null;
			}
			var existingShoppingCart = CartItems.FirstOrDefault(ci => ci?.Id == ShoppingCart.Id);
			if (existingShoppingCart != null) {
				product.Quantity += existingShoppingCart.Quantity;
			}
			if (ShoppingCart.Quantity > product.Quantity) {
				Console.WriteLine($"Insufficient stock. Only {product.Quantity} available.");
				return null;
			}
			product.Quantity -= ShoppingCart.Quantity;
			if (ShoppingCart.Id == 0) {
				ShoppingCart.Id = LastKey + 1;
				CartItems.Add(ShoppingCart);
			} else {
				var existing = CartItems.FirstOrDefault(ci => ci?.Id == ShoppingCart.Id);
				if (existing != null) {
					existing.Quantity = ShoppingCart.Quantity;
				}
			}
			return ShoppingCart;
		}
		public ShoppingCart? Delete(int id) {
			var item = CartItems.FirstOrDefault(ci => ci?.Id == id);
			if (item != null) {
				var product = ProductServiceProxy.Current.Inventory
					.FirstOrDefault(p => p?.Id == item.ProductId);
				if (product != null) {
					product.Quantity += item.Quantity;
				}
				CartItems.Remove(item);
			}
			return item;
		}

        public void Checkout() {
            if (!CartItems.Any()) {
                Console.WriteLine("No items in cart. Exiting...");
                return;
            }

            Console.WriteLine("------------- Receipt ------------- ");
            foreach (var item in CartItems) {
                if (item == null) continue;
                Console.WriteLine($"{item.Name} x {item.Quantity} total: ${item.Total:F2}");
            }

            Console.WriteLine($"Subtotal: ${Subtotal:F2}");
            Console.WriteLine($"Total (with 7% tax): ${Total:F2}");
            return;
        }

		public decimal Subtotal {
			get {
				decimal subtotal = 0;
				foreach (var item in CartItems) {
					if (item == null) continue;
					subtotal += item.Total;
				}
				return subtotal;
            }
			private set {}
		}

		public decimal Total {
			get {
				decimal taxRate = 0.07m;
				decimal tax = Subtotal * taxRate;
				return Subtotal + tax;
			}
			private set {}
		}

		public List<ShoppingCart?> CartItems { get; private set; }
	}
}