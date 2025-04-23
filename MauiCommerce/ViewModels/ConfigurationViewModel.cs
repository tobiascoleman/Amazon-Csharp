using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    public class ConfigurationViewModel
    {
        public string ? tax_input { get; set; }
        public CartServiceProxy svc = CartServiceProxy.Current;

        public void set_tax_input()
        {
            if (tax_input == null) { return; }
            svc.taxRate = double.Parse(tax_input);
        }
    
    }
}
