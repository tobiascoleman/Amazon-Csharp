using Library.eCommerce.Models;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class CartListView : ContentPage
{
    public CartListView()
	{
		InitializeComponent();
		BindingContext = new CartsViewModel();
	}

	public void AddNewShoppingCart(object sender, EventArgs e)
    {
        (BindingContext as CartsViewModel).AddNewCart();
    }

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as CartsViewModel)?.NotifyPropertyChanged(nameof(CartsViewModel.arrayOfShoppingCarts));
	}

	public void GoBackToShoppingCart(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//ShoppingManagement");
    }

	public void switchShoppingCart(object sender, EventArgs e)
	{
		(BindingContext as CartsViewModel).ChangeCurrentShoppingCart();
		// Go back to ShoppingManagement page
		Shell.Current.GoToAsync("//ShoppingManagement");
    }
}
