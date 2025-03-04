using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();//read all the data and display 
        Product GetProduct(int id);//read single customer details
        int AddProduct(Product product);//insert
        int UpdateProduct(int id, Product product);//update
        int DeleteProduct(int id);//delete
    }
}
