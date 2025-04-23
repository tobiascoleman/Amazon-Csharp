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
    public class CheckoutViewModel : INotifyPropertyChanged
    { 
        private CartListService _shopsvc = CartListService.Current;
    
        private CartServiceProxy? __svc;

        public event PropertyChangedEventHandler? PropertyChanged;

        public double CheckoutCost
        {
            get {
                __svc = _shopsvc.ReturnCurrentList() ?? throw new InvalidOperationException("ReturnCurrentList() returned null.");
                var rounded = Math.Round(__svc.CheckoutPrice + ((__svc.CheckoutPrice * __svc.taxRate) /100), 2);
                return rounded;
                }
            set 
            {
                if (__svc != null)
                {
                    __svc.CheckoutPrice = value;
                }
                else
                {
                    throw new InvalidOperationException("__svc is null.");
                }

            }
        }

        public ObservableCollection<CartItem?> ShoppingCart
        {
            get
            {
                __svc = _shopsvc.ReturnCurrentList() ?? throw new InvalidOperationException("ReturnCurrentList() returned null.");
                double totalCost = 0;
                foreach (var item in __svc.CartItems)
                {
                    totalCost += (item?.Price ?? 0) * (item?.Quantity ?? 0);
                }
                CheckoutCost = Math.Round(totalCost, 2);
                NotifyPropertyChanged(nameof(CheckoutCost));

                var toRet = new ObservableCollection<CartItem?>(__svc.CartItems
                    .Where(i => i?.Quantity > 0)
                );
                return toRet;
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

        public void RefreshUI()
        {
            NotifyPropertyChanged(nameof(CheckoutCost));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
        public void ClearOnCheckout()
        {
            var currentList = _shopsvc.ReturnCurrentList();
            if (currentList?.CartItems != null)
            {
                    currentList.CartItems.Clear();
            }
            CheckoutCost = 0;
        }
    }
}
