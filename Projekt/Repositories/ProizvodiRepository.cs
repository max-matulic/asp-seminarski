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
    }
}
