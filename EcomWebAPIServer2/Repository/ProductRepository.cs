using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcomContext db;

        public ProductRepository(EcomContext db)
        {
            this.db = db;
        }
        public int AddProduct(Product product)
        {
            db.Products.Add(product);
            return db.SaveChanges();
        }


        public int DeleteProduct(int id)
        {
            Product c = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            db.Products.Remove(c);
            return db.SaveChanges();
        }

        public Product GetProduct(int id)
        {
            return db.Products.Where(x => x.ProductId == id).FirstOrDefault();
        }

        public Product GetProductByName(string productname)
        {
            return db.Products.Where(x => x.ProductName == productname).FirstOrDefault();
        }



        public List<Product> GetProducts()
        {
            return db.Products.ToList();

        }

        public int UpdateProduct(int id, Product product)
        {
            Product c = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            c.ProductName = product.ProductName;
            c.ProductDescription = product.ProductDescription;
            c.ProductPrice = product.ProductPrice;
            c.ProductCategory = product.ProductCategory;
            c.ProductImgURL = product.ProductImgURL;
            db.Entry<Product>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
