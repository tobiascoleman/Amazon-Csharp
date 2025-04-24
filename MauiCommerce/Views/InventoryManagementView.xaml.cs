using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Delete();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var productId = (BindingContext as InventoryManagementViewModel)?.SelectedProduct?.Id;
        Shell.Current.GoToAsync($"//Product?productId={productId}");
    }

    private void InlineEditClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int productId)
        {
            Shell.Current.GoToAsync($"//Product?productId={productId}");
        }
    }

    private void InlineDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int productId)
        {
            (BindingContext as InventoryManagementViewModel)?.DeleteItem(productId);
        }
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }

    private void ChangeFilterClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.ChangeSort();
    }
}
