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
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;
        private ObservableCollection<CartItem?> _inventory;

        private string _sortOption = "Name";
        public string SortOption
        {
            get => _sortOption;
            set
            {
                if (_sortOption != value)
                {
                    _sortOption = value;
                    NotifyPropertyChanged();
                    UpdateInventory();
                }
            }
        }

        public InventoryManagementViewModel()
        {
            _inventory = new ObservableCollection<CartItem?>();
            UpdateInventory();
        }

        private void UpdateInventory()
        {
            var filteredList = _svc.Inventory.Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
            
            IEnumerable<CartItem?> orderedList;
            if (SortOption == "Name")
            {
                orderedList = filteredList.OrderBy(i => i?.Product?.Name);
            }
            else // "Price"
            {
                orderedList = filteredList.OrderBy(i => i?.Price);
            }

            _inventory.Clear();
            foreach (var item in orderedList)
            {
                _inventory.Add(item);
            }
            
            NotifyPropertyChanged(nameof(Inventory));
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

        public void RefreshProductList()
        {
            UpdateInventory();
        }

        public ObservableCollection<CartItem?> Inventory
        {
            get => _inventory;
        }

        public CartItem? DeleteItem(int itemId)
        {
            var item = _svc.Delete(itemId);
            UpdateInventory();
            return item;
        }
        
        public void ChangeSort() {
            SortOption = SortOption == "Name" ? "Price" : "Name";
        }
    }
}
