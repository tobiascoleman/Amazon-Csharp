using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;

namespace Library.eCommerce.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public CartServiceProxy? ShopService { get; set; }

        public ShoppingCart(int newId, CartServiceProxy svc)
        {
            Id = newId;
            ShopService = svc;
        }

    }
}
