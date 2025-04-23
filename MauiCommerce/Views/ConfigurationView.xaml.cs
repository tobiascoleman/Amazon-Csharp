using Maui.eCommerce.ViewModels;

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
        (BindingContext as ConfigurationViewModel).set_tax_input();
        Shell.Current.GoToAsync("//MainPage");
    }
    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}
