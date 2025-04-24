using System.ComponentModel;
using Maui.eCommerce.ViewModels;
namespace Maui.eCommerce.Views;


public partial class CheckoutView : ContentPage
{
    public CheckoutView()
	{
		InitializeComponent();
        BindingContext = new CheckoutViewModel();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CheckoutViewModel)?.RefreshUI();
    }

    public void ReturnClicked(object sender, EventArgs e)
    {
        (BindingContext as CheckoutViewModel).RefreshUI();
        Shell.Current.GoToAsync($"//MainPage");        
    }

    public void CheckOutClicked(object sender, EventArgs e)
    {
        (BindingContext as CheckoutViewModel).RefreshUI();
        (BindingContext as CheckoutViewModel).ClearOnCheckout();
        
        // Navigate back to shopping management
        Shell.Current.GoToAsync("//ShoppingManagement");
    }
}
