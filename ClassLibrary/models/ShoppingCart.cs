using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;

namespace Library.eCommerce.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Cart"; // Adding a name property
        public CartServiceProxy? CartService { get; set; }

        // Add properties for cart item count and total cost
        public int ItemCount 
        { 
            get
            {
                if (CartService == null)
                    return 0;
                
                return CartService.CartItems?.Count ?? 0;
            }
        }

        public double TotalCost
        {
            get
            {
                if (CartService == null)
                    return 0;
                
                return CartService.CartItems?.Sum(item => item.Price * item.Quantity) ?? 0;
            }
        }

        public ShoppingCart(int newId, CartServiceProxy svc)
        {
            Id = newId;
            CartService = svc;
            Name = "Cart"; // Default name
        }

        public ShoppingCart(int newId, CartServiceProxy svc, string name)
        {
            Id = newId;
            CartService = svc;
            Name = name;
        }
    }
}
