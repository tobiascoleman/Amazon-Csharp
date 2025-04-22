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
        public CartItem? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;

        public string AddQuantity { get; set; } = "1";

        public Command<CartItem> AddToCartCommand { get; }

        public InventoryManagementViewModel()
        {
            AddToCartCommand = new Command<CartItem>(AddToCart);
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
            NotifyPropertyChanged(nameof(Inventory));
        }

        public ObservableCollection<CartItem?> Inventory
        {
            get
            {
                var filteredList = _svc.Inventory.Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                return new ObservableCollection<CartItem?>(filteredList);
            }
        }

        public CartItem? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Inventory");
            return item;
        }

        private void AddToCart(CartItem item)
        {
            if (int.TryParse(AddQuantity, out int quantity) && quantity > 0)
            {
                for (int i = 0; i < quantity; i++)
                {
                    CartServiceProxy.Current.AddOrUpdate(item);
                }
                NotifyPropertyChanged(nameof(Inventory));
            }
        }

        public void SortInventoryByName()
        {
            var sortedList = _svc.Inventory.OrderBy(p => p?.Product?.Name).ToList();
            _svc.Inventory = new ObservableCollection<CartItem?>(sortedList);
            NotifyPropertyChanged(nameof(Inventory));
        }

        public void SortInventoryByPrice()
        {
            var sortedList = _svc.Inventory.OrderBy(p => p?.Product?.Price).ToList();
            _svc.Inventory = new ObservableCollection<CartItem?>(sortedList);
            NotifyPropertyChanged(nameof(Inventory));
        }
    }
}
