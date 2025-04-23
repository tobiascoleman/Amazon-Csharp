using System;
using Maui.eCommerce.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiCommerce
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }

        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingManagement");
        }

        private void ConfigurationClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Configuration");
        }
    }

}
