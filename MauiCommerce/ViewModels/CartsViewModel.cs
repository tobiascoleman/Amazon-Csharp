using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class CartsViewModel : INotifyPropertyChanged
    {
        public ShoppingCart? SelectedCart { get; set; }
        public string NewCartName { get; set; } = "New Cart";

        public CartListService cartSvc { get; set; } = CartListService.Current;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ShoppingCart?> arrayOfShoppingCarts
        {
            get
            {
                return new ObservableCollection<ShoppingCart?>(cartSvc.Carts.Cast<ShoppingCart?>().ToList());
            }
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void AddNewCart()
        {
            cartSvc.AddToShoppingCart(NewCartName);
            NewCartName = "New Cart"; // Reset to default
            NotifyPropertyChanged(nameof(arrayOfShoppingCarts));
            NotifyPropertyChanged(nameof(NewCartName));
        }

        public void ChangeCurrentShoppingCart()
        {
            if (SelectedCart != null)
            {
                cartSvc.currentCartId = SelectedCart.Id;
                NotifyPropertyChanged(nameof(arrayOfShoppingCarts));
                NotifyPropertyChanged(nameof(SelectedCart));
                NotifyPropertyChanged(nameof(cartSvc));
            }
        }

        // Helper methods to get cart item count and total cost
        public int GetCartItemCount(ShoppingCart cart)
        {
            if (cart?.CartService == null)
                return 0;

            return cart.CartService.CartItems.Count(item => item.Quantity > 0);
        }

        public double GetCartTotalCost(ShoppingCart cart)
        {
            if (cart?.CartService == null)
                return 0;

            return cart.CartService.CheckoutPrice;
        }
    }
}
