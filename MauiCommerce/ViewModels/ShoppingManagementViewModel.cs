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
        private CartListService _cartListSvc = CartListService.Current;
       public CartItem? SelectedItem { get; set; }
       public CartItem? SelectedCartItem { get; set; }
        public string SortOption = "Name";

        public ObservableCollection<CartItemViewModel?> Inventory
        {
            get
            {
                var filteredList = _invSvc.Inventory.Where(i => i?.Quantity > 0)
                    .Where(i => i != null)
                    .Select(i => new CartItemViewModel(i!));
                switch (SortOption)
                {
                    case "Name":
                        return new ObservableCollection<CartItemViewModel?>(filteredList.OrderBy(i => i?.Model.Product?.Name));
                    case "Price":
                        return new ObservableCollection<CartItemViewModel?>(filteredList.OrderBy(i => i?.Model.Price));
                    default:
                        return new ObservableCollection<CartItemViewModel?>(filteredList);
                }
                
            }
        }

        public ObservableCollection<CartItemViewModel?> ShoppingCart
        {
            get
            {
                var filteredList = _cartListSvc.ReturnCurrentList()?.CartItems?.Where(i => i?.Quantity > 0)
                    .Where(i => i != null)
                    .Select(i => new CartItemViewModel(i!));
                switch (SortOption)
                {
                    case "Name":
                        return new ObservableCollection<CartItemViewModel?>(filteredList.OrderBy(i => i?.Model.Product?.Name));
                    case "Price":
                        return new ObservableCollection<CartItemViewModel?>(filteredList.OrderBy(i => i?.Model.Price));
                    default:
                        return new ObservableCollection<CartItemViewModel?>(filteredList);
                }
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

        public void ChangeSort() {
            if (SortOption == "Name")
            {
                SortOption = "Price";
            }
            else
            {
                SortOption = "Name";
            }
            NotifyPropertyChanged(nameof(Inventory));
        }
    }
}
