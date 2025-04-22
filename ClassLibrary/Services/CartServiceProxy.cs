using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class CartServiceProxy
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<CartItem> items;
        public double CheckoutPrice;
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
                if(instance == null)
                {
                    instance = new CartServiceProxy();
                }

                return instance;
            } 
        }
        private static CartServiceProxy? instance;
        private CartServiceProxy() { 
            items = new List<CartItem>();
        }

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
                if (_prodSvc != null && _prodSvc.Inventory != null && itemToReturn != null)
                {
                    var inventoryItem = _prodSvc.Inventory.FirstOrDefault(p => p != null && p.Id == itemToReturn.Id);
                    if (inventoryItem == null)
                    {
                        _prodSvc.AddOrUpdate(new CartItem(itemToReturn));
                    }
                    else
                    {
                        inventoryItem.Quantity++;
                    }
                }
            }


            return itemToReturn;
        }

        public void AddToWishlist(CartItem item)
        {
            if (item == null || item.Id <= 0)
            {
                throw new ArgumentException("Invalid cart item.");
            }

            // Logic to add the item to a wishlist (not implemented in the current context)
            // For now, we can log or simulate the addition.
            Console.WriteLine($"Item with ID {item.Id} added to wishlist.");
        }
        public void SortCartItems(Comparison<CartItem> comparison)
        {
            if (items != null)
            {
                items.Sort(comparison);
            }
        }
    }
}
