using eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy {
        private ProductServiceProxy() {
            Inventory = new List<Product?>();
        }

        private int LastKey {
            get {
                if(Inventory.Count == 0) {
                    return 0;
                }
                return Inventory.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current {
            get {
                lock(instanceLock) {
                    instance ??= new ProductServiceProxy();
                }
                return instance;
            }
        }

        public List<Product?> Inventory { get; private set; }


        public Product AddOrUpdate(Product product) {
            if(product.Id == 0) {
                product.Id = LastKey + 1;
                Inventory.Add(product);
            }
            return product;
        }

        public Product? Delete(int id) {
            if(id == 0) {
                return null;
            }

            Product? product = Inventory.FirstOrDefault(p => p?.Id == id);
            Inventory.Remove(product);

            return product;
        }
    }
}