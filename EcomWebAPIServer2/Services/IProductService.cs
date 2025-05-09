﻿using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();//read all the data and display 
        Product GetProduct(int id);//read single customer details
        int AddProduct(Product product);//insert
        int UpdateProduct(int id, Product user);//update
        int DeleteProduct(int id);//delete


        Product GetProductByName(string productName);

        Task<List<Product>> SearchProductsAsync(string query); // Search products by name 
    }
}
