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

	public void GoBackToShoppingCart(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//ShoppingManagement");
    }

	public void switchShoppingCart(object sender, EventArgs e)
	{
		(BindingContext as CartsViewModel).ChangeCurrentShoppingCart();
    }
}
