using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel)?.RefreshUI();
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
        (BindingContext as ShoppingManagementViewModel).RefreshUI();
    }
    
    private void InlineRemoveClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).RefreshUI();
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
    }

    public void OnCancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

}
