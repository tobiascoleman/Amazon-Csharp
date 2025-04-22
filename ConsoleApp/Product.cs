//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace MyApp
{
    internal class Program
    {
        static void InventoryManagement() {
			Console.WriteLine("Welcome to the Inventory Manager!");
			
			List<Product?> list = ProductServiceProxy.Current.Inventory;

			char choice;
			do {
                Console.WriteLine("C. Create new inventory item");
                Console.WriteLine("R. Read all inventory items");
                Console.WriteLine("U. Update an inventory item");
                Console.WriteLine("D. Delete an inventory item");
                Console.WriteLine("Q. Back to Menu");
                Console.Write("Choose an Option: ");
				string? input = Console.ReadLine() ?? "";
				choice = input[0];
                choice = char.ToLower(choice);
				switch (choice) {
					case 'c':
						Console.Write("Enter product name: ");
						string name = Console.ReadLine() ?? string.Empty;
						Console.Write("Enter product price: ");
						decimal price = decimal.Parse(Console.ReadLine() ?? "0");
						Console.Write("Enter product quantity: ");
						int quantity = int.Parse(Console.ReadLine() ?? "0");

						ProductServiceProxy.Current.AddOrUpdate(
                            new Product{
                                Name = name,
                                Price = price,
                                Quantity = quantity
                            });
						break;
					case 'r':
						list.ForEach(Console.WriteLine);
						break;
					case 'u':
						Console.Write("Which product to update?: ");
						int id = int.Parse(Console.ReadLine() ?? "-1");
						var selectedProd = list.FirstOrDefault(p => p.Id == id);

						if (selectedProd != null) {
                            Console.Write("What would you like to update? (name, price, quantity, all): ");
                            string selection = Console.ReadLine() ?? string.Empty;
                            selection = selection.ToLower();
                            switch (selection) {
                                case "name": 
                                    Console.Write($"New name ({selectedProd.Name}): ");
                                    var newName = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newName))
                                        selectedProd.Name = newName;
                                    if(selection == "all")
                                        goto case "price";
                                    break; 
                                case "price":
                                    Console.Write($"New price ({selectedProd.Price}): ");
                                    if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                                        selectedProd.Price = newPrice;
                                    if(selection == "all")
                                        goto case "quantity";
                                    break;
                                case "quantity":
                                    Console.Write($"New quantity ({selectedProd.Quantity}): ");
                                    if (int.TryParse(Console.ReadLine(), out int newQty))
                                        selectedProd.Quantity = newQty;
                                    break;
                            }
							ProductServiceProxy.Current.AddOrUpdate(selectedProd);
						}
						break;
					case 'd':
						Console.Write("Which product would you like to delete?: ");
						id = int.Parse(Console.ReadLine() ?? "-1");
						ProductServiceProxy.Current.Delete(id);
						break;
					case 'q':
						break;
					default:
						Console.WriteLine("Error: Unknown Command");
						break;
				}
                Console.WriteLine("Success!\n");
			} while (choice != 'Q' && choice != 'q');
		}

		static void Shopping() {
			Console.WriteLine("Enjoy Shopping!");

			var cartService = CartServiceProxy.Current;
			var productService = ProductServiceProxy.Current;

			char choice = '0';
            do {
				Console.WriteLine("C. Add Item to Cart");
				Console.WriteLine("R. List Items in Cart");
				Console.WriteLine("U. Update Item in Cart");
				Console.WriteLine("D. Delete Item From Cart");
				Console.WriteLine("I. View Inventory");
				Console.WriteLine("B. Checkout and return to Main Menu");
                Console.WriteLine("Q. Back to Main Menu");
				Console.Write("Choose an option: ");

				string? input = Console.ReadLine() ?? "";
				choice = input[0];
                choice = char.ToLower(choice);

				switch (choice) {
					case 'c':
						Console.Write("Enter Product ID: ");
						int productId = int.Parse(Console.ReadLine() ?? "-1");
						var product = productService.Inventory.FirstOrDefault(p => p.Id == productId);
						if (product == null) {
							Console.WriteLine("Product not found.");
							break;
						}

						Console.Write("Enter Quantity: ");
						int qty = int.Parse(Console.ReadLine() ?? "-1");
						if (qty <= 0) {
							Console.WriteLine("Invalid quantity.");
							break;
						}

						cartService.AddOrUpdate(
                            new CartItem {
                                ProductId = productId,
                                Name = product.Name,
                                Quantity = qty,
                                Price = product.Price
                            });
						break;
					case 'r':
						Console.WriteLine("\nYour Cart:");
						cartService.CartItems.ForEach(item => Console.WriteLine(item));
						break;
					case 'u':
						Console.Write("Enter Cart Item ID to update: ");
						int cartId = int.Parse(Console.ReadLine() ?? "0");
						var cartItem = cartService.CartItems.FirstOrDefault(ci => ci.Id == cartId);
						if (cartItem == null) {
							Console.WriteLine("Item not found.");
							break;
						}

						Console.Write("Enter New Quantity: ");
						int newQty = int.Parse(Console.ReadLine() ?? "0");
						cartItem.Quantity = newQty;
						cartService.AddOrUpdate(cartItem);
						break;
					case 'd':
						Console.Write("Enter Cart Item ID to remove: ");
						int removeId = int.Parse(Console.ReadLine() ?? "0");
						cartService.Delete(removeId);
						break;
					case 'i':
						Console.WriteLine("\nCurrent Inventory:");
						productService.Inventory.ForEach(p => Console.WriteLine(p));
						break;
                    case 'b':
                        cartService.Checkout();
						return;
					case 'q':
						break;
					default:
						Console.WriteLine("Error: Unknown Command.");
						break;
				}
                Console.WriteLine("Success!\n");

			}while(choice != 'q');
		}

		static void Main(string[] args) {
			char choice;
			do {
                Console.WriteLine("Welcome to Amazon");
                Console.WriteLine("M. Manage the inventory");
                Console.WriteLine("S. Go shopping");
                Console.WriteLine("Q. Quit");
                Console.Write("Choose an Option: ");
				string? input = Console.ReadLine() ?? "";
				choice = input[0];
                choice = char.ToLower(choice);
				switch (choice) {
					case 'm':
						InventoryManagement();
						break;
					case 's':
						Shopping();
						break;
					case 'q':
						break;
					default:
						Console.WriteLine("Error: Unknown Command");
						break;
				}
			} while (choice != 'Q' && choice != 'q');
		}
    }
}
