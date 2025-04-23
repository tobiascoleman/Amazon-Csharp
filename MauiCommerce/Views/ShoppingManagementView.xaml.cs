using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}

    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnItem();
    }
    private void AddToCartClicked(object sender, EventArgs e)
    {
		(BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }

    private void InlineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).RefreshUX();
    }

	private async void OnCheckoutClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//Checkout");
	}

    private async void OnChangeCartClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//CartList");
	}

    public void ChangeFilterClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ChangeSort();
        (BindingContext as ShoppingManagementViewModel).RefreshUX();
    }

}
