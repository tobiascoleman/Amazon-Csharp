using Library.eCommerce.Models;
using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.eCommerce.ViewModels
{
    public class CartItemViewModel
    {
        public CartItem Model { get; set; }

        public string? amount { get; set; } = "1";

        public ICommand? AddCommand { get; set; }

        public ICommand? AddCustomAmountClicked { get; set; }

        private ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private CartListService _cartListSvc = CartListService.Current;
        
        // Get the current cart service
        private CartServiceProxy? CurrentCart => _cartListSvc.ReturnCurrentList();

        private void DoAdd()
        {
            CurrentCart?.AddOrUpdate(Model);
        }

        private void AddCustomInCart()
        {
            if (amount == null ) { return ; }
            int value = int.Parse(amount);

            if (value < 1) { return; }

            for (int i = 0; i < value; i++)
            {
                CurrentCart?.AddOrUpdate(Model);
            }
        }

        void SetupCommands()
        {
            AddCommand = new Command(DoAdd);
            AddCustomAmountClicked = new Command(AddCustomInCart);
        }

        public CartItemViewModel()
        {
            Model = new CartItem();

            SetupCommands();
        }

        public CartItemViewModel(CartItem model)
        {
            Model = model;
            SetupCommands();
        }
    }
}
