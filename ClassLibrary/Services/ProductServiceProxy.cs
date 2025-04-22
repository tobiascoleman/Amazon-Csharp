using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Inventory = new List<CartItem?>
            {
                new CartItem{ Product = new ProductDTO{Id = 1, Name ="Product 1"}, Id = 1, Quantity = 1, Price = 5 },
                new CartItem{ Product = new ProductDTO{Id = 2, Name ="Product 2"}, Id = 2 , Quantity = 2,Price = 50 },
                new CartItem{ Product = new ProductDTO{Id = 3, Name ="Product 3"}, Id=3 , Quantity = 3, Price = 500 }
            };
        }

        private int LastKey
        {
            get
            {
                if(!Inventory.Any())
                {
                    return 0;
                }

                return Inventory.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<CartItem?> Inventory { get; private set; }


        public CartItem AddOrUpdate(CartItem item)
        {
            if(item.Id == 0)
            {
                item.Id = LastKey + 1;
                item.Product.Id = item.Id;
                Inventory.Add(item);
            } else
            {
                var existingItem = Inventory.FirstOrDefault(p => p.Id == item.Id);
                var index = Inventory.IndexOf(existingItem);
                Inventory.RemoveAt(index);
                Inventory.Insert(index,new CartItem(item));
            }


            return item;
        }

        public CartItem? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            CartItem? product = Inventory.FirstOrDefault(p => p.Id == id);
            Inventory.Remove(product);

            return product;
        }

        public CartItem? GetById(int id)
        {
            return Inventory.FirstOrDefault(p => p.Id == id);
        }

    }

    
}
