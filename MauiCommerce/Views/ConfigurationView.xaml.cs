using Maui.eCommerce.ViewModels;
using Library.eCommerce.Services;

namespace Maui.eCommerce.Views;

public partial class ConfigurationView : ContentPage
{
    public ConfigurationView()
    {
        InitializeComponent();
        BindingContext = new ConfigurationViewModel();
    }

    public void SetTaxClicked(object sender, EventArgs e)
    {
        var viewModel = (BindingContext as ConfigurationViewModel);
        viewModel.set_tax_input();
        
        // Refresh all pages that might display the tax rate
        RefreshAllViewsWithTaxRate();
        
        Shell.Current.GoToAsync("//MainPage");
    }
    
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    
    private void RefreshAllViewsWithTaxRate()
    {
        // This method will force all checkout views to refresh their tax rate display
        // by calling RefreshUI on any currently displayed checkout view
        if (Shell.Current.CurrentPage is CheckoutView checkoutView && 
            checkoutView.BindingContext is CheckoutViewModel checkoutViewModel)
        {
            checkoutViewModel.RefreshUI();
        }
    }
}
