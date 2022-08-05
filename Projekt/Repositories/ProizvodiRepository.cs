using Projekt.Data;
using Projekt.Interface;
using Projekt.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projekt.Repositories
{
    public class ProizvodiRepository : IProizvodiRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProizvodiRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAll()
        {
            return _dbContext.Product.ToList();
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Product.FirstOrDefault(p => p.Id == id);
        }

        public Product InsertProduct(Product product)
        {
            var result = _dbContext.Product.Add(product);
            _dbContext.SaveChanges();

            return result.Entity;
        }

        public Product UpdateProduct(Product product)
        {
            var result = _dbContext.Product.FirstOrDefault(p => p.Id == product.Id);

            if(result != null)
            {
                result.Title = product.Title;
                result.Description = product.Description;
                result.Price = product.Price;

                _dbContext.SaveChanges();

                return result;
            }

            return null;
        }

        public void DeleteProduct(int id)
        {
            var result = _dbContext.Product.FirstOrDefault(p => p.Id == id);

            if(result != null)
            {
                _dbContext.Product.Remove(result);
                _dbContext.SaveChanges();
            }
        }
    }
}
