using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
           if(product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbentry = context.Products.Find(product.ProductID);
                if(dbentry != null)
                {
                    dbentry.Name = product.Name;
                    dbentry.Description = product.Description;
                    dbentry.Price = product.Price;
                    dbentry.Category = product.Category;
                    dbentry.ImageData = product.ImageData;
                    dbentry.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);
            if(dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
