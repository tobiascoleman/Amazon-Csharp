using System.ComponentModel;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class CartServiceProxy
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<CartItem> items;
        public double CheckoutPrice;

        public double taxRate { get; set; }
        public List<CartItem> CartItems
        {
            get
            {
                return items;
            }
        }
        public static CartServiceProxy Current {
            get
            {
                if (instance == null)
                {
                    instance = new CartServiceProxy();
                }

                return instance;
            }
        }
        private static CartServiceProxy? instance;

        private CartServiceProxy() {
            items = new List<CartItem>();
            taxRate = 5;
        }

        public static CartServiceProxy? AddNewShoppingCart()
        {
            CartServiceProxy cartService = new CartServiceProxy();
            return cartService;
        }
        public void ClearList() { items = new List<CartItem>(); }
        //Sets the list to an empty, new, list

        public CartItem? AddOrUpdate(CartItem item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if(existingInvItem == null || existingInvItem.Quantity == 0) {
                return null;
            }

            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem == null)
            {
                //add
                var newItem = new CartItem(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);
            } else
            {
                //update
                existingItem.Quantity++;
            }


            return existingInvItem;
        }

        public CartItem? ReturnItem(CartItem? item)
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }

            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Inventory.FirstOrDefault(p => p.Id == itemToReturn.Id); ;
                if(inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new CartItem(itemToReturn));
                } else
                {
                    inventoryItem.Quantity++;
                }
            }
            return itemToReturn;
        }
    }
}
