using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        private string? _taxInput;
        public event PropertyChangedEventHandler? PropertyChanged;

        public CartServiceProxy Svc { get; } = CartServiceProxy.Current;

        public string? tax_input
        { 
            get
            {
                if (_taxInput == null)
                {
                    // Initialize with the current global tax rate
                    _taxInput = Svc.taxRate.ToString();
                }
                return _taxInput;
            }
            set
            {
                _taxInput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(tax_input)));
            }
        }

        public void set_tax_input()
        {
            if (tax_input == null) { return; }
            
            if (double.TryParse(tax_input, out double taxRate))
            {
                // Update the global tax rate for all carts
                CartServiceProxy.SetGlobalTaxRate(taxRate);
            }
        }
    }
}
