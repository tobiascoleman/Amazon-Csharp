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
        // Keep reference to CartListService only
        private CartListService _cartListSvc = CartListService.Current;
        private string _sortOption = "Name";
        private ObservableCollection<CartItemViewModel?> _inventory;
        private ObservableCollection<CartItemViewModel?> _shoppingCart;
        
        public CartItem? SelectedItem { get; set; }
        public CartItem? SelectedCartItem { get; set; }
        
        // Property to get the current cart service
        private CartServiceProxy? CurrentCart => _cartListSvc.ReturnCurrentList();
        
        public string SortOption
        {
            get => _sortOption;
            set
            {
                if (_sortOption != value)
                {
                    _sortOption = value;
                    NotifyPropertyChanged();
                    UpdateCollections();
                }
            }
        }

        public ShoppingManagementViewModel()
        {
            _inventory = new ObservableCollection<CartItemViewModel?>();
            _shoppingCart = new ObservableCollection<CartItemViewModel?>();
            UpdateCollections();
        }

        private void UpdateCollections()
        {
            // Update Inventory
            var filteredInventory = _invSvc.Inventory
                .Where(i => i?.Quantity > 0)
                .Where(i => i != null)
                .Select(i => new CartItemViewModel(i!));

            IEnumerable<CartItemViewModel?> orderedInventory;
                
            if (SortOption == "Name")
            {
                orderedInventory = filteredInventory.OrderBy(i => i?.Model.Product?.Name);
            }
            else // "Price"
            {
                orderedInventory = filteredInventory.OrderBy(i => i?.Model.Price);
            }

            _inventory.Clear();
            foreach (var item in orderedInventory)
            {
                _inventory.Add(item);
            }
            
            // Update ShoppingCart
            var filteredCart = CurrentCart?.CartItems?
                .Where(i => i?.Quantity > 0)
                .Where(i => i != null)
                .Select(i => new CartItemViewModel(i!));

            if (filteredCart != null)
            {
                IEnumerable<CartItemViewModel?> orderedCart;
                
                if (SortOption == "Name")
                {
                    orderedCart = filteredCart.OrderBy(i => i?.Model.Product?.Name);
                }
                else // "Price"
                {
                    orderedCart = filteredCart.OrderBy(i => i?.Model.Price);
                }

                _shoppingCart.Clear();
                foreach (var item in orderedCart)
                {
                    _shoppingCart.Add(item);
                }
            }
            
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }

        public ObservableCollection<CartItemViewModel?> Inventory
        {
            get => _inventory;
        }

        public ObservableCollection<CartItemViewModel?> ShoppingCart
        {
            get => _shoppingCart;
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

        public void RefreshUI()
        {
            UpdateCollections();
        }

        public void PurchaseItem()
        {
            if (SelectedItem != null && CurrentCart != null)
            {
                var shouldRefresh = SelectedItem.Quantity >= 1;
                var updatedItem = CurrentCart.AddOrUpdate(SelectedItem);

                if(updatedItem != null && shouldRefresh) {
                    UpdateCollections();
                }
            }
        }

        public void ReturnItem()
        {
            if (SelectedCartItem != null && CurrentCart != null) {
                var shouldRefresh = SelectedCartItem.Quantity >= 1;
                
                var updatedItem = CurrentCart.ReturnItem(SelectedCartItem);

                if (updatedItem != null && shouldRefresh)
                {
                    UpdateCollections();
                }
            }
        }

        public void ChangeSort() {
            SortOption = SortOption == "Name" ? "Price" : "Name";
        }
    }
}
