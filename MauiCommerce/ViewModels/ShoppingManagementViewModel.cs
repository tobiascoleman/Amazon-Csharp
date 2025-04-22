using Library.eCommerce.Models;
using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingManagementViewModel : INotifyPropertyChanged
    {
        private ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private CartServiceProxy _cartSvc = CartServiceProxy.Current;
       public CartItem? SelectedItem { get; set; }
       public CartItem? SelectedCartItem { get; set; }

        public ObservableCollection<CartItem?> Inventory
        {
            get
            {
                return new ObservableCollection<CartItem?>(_invSvc.Inventory
                    .Where(i => i?.Quantity > 0)
                    );
            }
        }

        public ObservableCollection<CartItem?> ShoppingCart
        {
            get
            {
                return new ObservableCollection<CartItem?>(_cartSvc.CartItems
                    .Where(i => i?.Quantity > 0)
                    );
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }

        public void PurchaseItem()
        {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.Quantity >= 1;
                var updatedItem = _cartSvc.AddOrUpdate(SelectedItem);

                if(updatedItem != null && shouldRefresh) {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }

            }
        }

        public void ReturnItem()
        {
            if (SelectedCartItem != null) {
                var shouldRefresh = SelectedCartItem.Quantity >= 1;
                
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem);

                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }

        public void SortCartByName()
        {
            _cartSvc.SortCartItems((x, y) => string.Compare(x?.Product?.Name, y?.Product?.Name, StringComparison.Ordinal));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }

        public void SortCartByPrice()
        {
            _cartSvc.SortCartItems((x, y) => x?.Product?.Price.CompareTo(y?.Product?.Price) ?? 0);
            NotifyPropertyChanged(nameof(ShoppingCart));
        }

        public void AddToWishlist()
        {
            if (SelectedCartItem != null)
            {
                _cartSvc.AddToWishlist(SelectedCartItem);
                NotifyPropertyChanged(nameof(ShoppingCart));
            }
        }
    }
}
