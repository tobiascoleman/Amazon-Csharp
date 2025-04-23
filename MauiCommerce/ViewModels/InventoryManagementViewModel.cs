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

        public string SortOption = "Name";

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
                
                switch (SortOption)
                {
                    case "Name":
                        filteredList = filteredList.OrderBy(I => I?.Product?.Name);
                        break;
                    case "Price":
                        filteredList = filteredList.OrderBy(I => I?.Price);
                        break;
                }
                return new ObservableCollection<CartItem?>(filteredList);
            }
        }

        public CartItem? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Inventory");
            return item;
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
