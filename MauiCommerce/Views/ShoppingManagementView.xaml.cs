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
		await Shell.Current.GoToAsync("CheckoutPage");
	}

    private void SortCartByChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var selectedSort = picker?.SelectedItem as string;

        if (selectedSort == "Name")
        {
            (BindingContext as ShoppingManagementViewModel)?.SortCartByName();
        }
        else if (selectedSort == "Price")
        {
            (BindingContext as ShoppingManagementViewModel)?.SortCartByPrice();
        }
    }

    private void AddToWishlistClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel)?.AddToWishlist();
    }
}
