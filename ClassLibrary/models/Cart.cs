using Library.eCommerce.DTO;
using System.Windows.Input;
using Library.eCommerce.Services;
using System.Data;

namespace Library.eCommerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Quantity { get; set; }

        public int Price { get; set; } 

        public ICommand? AddCommand { get; set; }

        public override string ToString()
        {
            return $"{Product} Quantity:{Quantity}";
        }

        public string Display { 
            get
            {
                return $"{Product?.Display ?? string.Empty} x{Quantity} ${Price}";
            }
        }

        public CartItem()
        {
            Product = new ProductDTO();
            Quantity = 0;

            AddCommand = new Command(DoAdd);
        }

        private void DoAdd()
        {
            CartServiceProxy.Current.AddOrUpdate(this);
        }

        public CartItem(CartItem i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
            Price = i.Price;

            AddCommand = new Command(DoAdd);
        }
    }
}
