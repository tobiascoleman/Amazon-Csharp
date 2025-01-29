﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Products = new List<Product?>();
        }

        private int LastKey
        {
            get
            {
                if(Products.Count == 0)
                {
                    return 0;
                }

                return Products.Select(p => p?.Id ?? 0).Max();
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
                    instance ??= new ProductServiceProxy();
                }

                return instance;
            }
        }

        public List<Product?> Products { get; private set; }


        public Product AddOrUpdate(Product product)
        {
            if(product.Id == 0)
            {
                product.Id = LastKey + 1;
                Products.Add(product);
            }


            return product;
        }

        public Product? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            Product? product = Products.FirstOrDefault(p => p?.Id == id);
            Products.Remove(product);

            return product;
        }

    }

    
}