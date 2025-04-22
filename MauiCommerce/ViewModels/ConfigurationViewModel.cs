using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Maui.eCommerce.ViewModels
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        private string _taxRate;

        public string TaxRate
        {
            get => _taxRate;
            set
            {
                if (_taxRate != value)
                {
                    _taxRate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand SaveTaxRateCommand { get; }

        public ConfigurationViewModel()
        {
            SaveTaxRateCommand = new Command(SaveTaxRate);
        }

        private void SaveTaxRate()
        {
            // Save the tax rate to a persistent storage or service
            Application.Current.Properties["TaxRate"] = TaxRate;
            Application.Current.SavePropertiesAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
