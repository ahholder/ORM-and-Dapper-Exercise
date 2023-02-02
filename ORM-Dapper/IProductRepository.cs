using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        //void InsertProduct(string newProductName, decimal newPrice, int newID, int newSale, string newStock);
        void InsertProduct(string newProductName, double newPrice, int newID);
        void UpdateProductName(int productID, string newName);
        void DeleteProduct(int productID);
    }
}
