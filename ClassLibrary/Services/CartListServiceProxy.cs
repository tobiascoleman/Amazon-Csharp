using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class CartListService
    {
        public List<ShoppingCart> Carts { get; private set; }
        public int currentCartId { get; set; } = 0;

        public static CartListService Current 
        { 
            get
            {
                if (instance == null)
                {
                    instance = new CartListService();
                }
                return instance;
            }
        } 
        private static CartListService? instance;

        private CartListService() { 
            Carts =
            [
                new ShoppingCart(0, CartServiceProxy.Current, "Default"),
                new ShoppingCart(1, CartServiceProxy.AddNewShoppingCart(), "Wishlist"),
            ];
        }

        public int GetNextId()
        {
            return Carts.Max(p => p.Id) + 1;
        }

        public void AddToShoppingCart(string name = "New Cart")
        {
            CartServiceProxy? newService = CartServiceProxy.AddNewShoppingCart();
            Carts.Add(new ShoppingCart(GetNextId(), newService, name));
        }

        public CartServiceProxy? ReturnCurrentList()
        {
            return Carts[currentCartId]?.CartService;
        }

    }
}
