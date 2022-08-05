using Projekt.Models;
using System.Collections.Generic;

namespace Projekt.Interface
{
    public interface IProizvodiRepository
    {
        List<Product> GetAll();
        Product GetProductById(int id);
        Product InsertProduct(Product product); 
        Product UpdateProduct(Product product);
        void DeleteProduct(int id); 
    }
}
