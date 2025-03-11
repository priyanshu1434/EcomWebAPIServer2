using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Repository;

namespace EcomWebAPIServer2.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repo;

        public ProductService(IProductRepository repo)
        {
            this.repo = repo;
        }
        public int AddProduct(Product product)
        {
            if (repo.GetProduct(product.ProductId) != null)
            {
                throw new ProductAlreadyExistsException($"Product with product id {product.ProductId} already exists");
            }
            return repo.AddProduct(product);
        }

        public int DeleteProduct(int id)
        {
            if (repo.GetProduct(id) == null)
            {

                throw new ProductNotFoundException($"Product with product id {id} does not exists");
            }
            return repo.DeleteProduct(id);
        }

        public Product GetProduct(int id)
        {
            Product c = repo.GetProduct(id);
            if (c == null)
            {
                throw new ProductNotFoundException($"Product with product id {id} does not exists");
            }
            return c;
        }

        public Product GetProductByName(string productName)
        {
            Product c = repo.GetProductByName(productName);
            if (c == null)
            {
                throw new ProductNotFoundException($"Product with product name {productName} does not exists");
            }
            return c;
        }

        public List<Product> GetProducts()
        {
            return repo.GetProducts();
        }


        public int UpdateProduct(int id, Product order)
        {
            if (repo.GetProduct(id) == null)
            {
                throw new ProductNotFoundException($"Product with product id {id} does not exists");
            }
            return repo.UpdateProduct(id, order);
        }


    }
}
