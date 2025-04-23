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
            cartSvc.AddToShoppingCart();
            NotifyPropertyChanged(nameof(arrayOfShoppingCarts));
        }

        public void ChangeCurrentShoppingCart()
        {
            if (SelectedCart != null)
            {
                cartSvc.currentCartId = SelectedCart.Id;
            }
        }
    }
}
