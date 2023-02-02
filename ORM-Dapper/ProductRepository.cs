using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection; //field
        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }
        //public void InsertProduct(string newProductName, decimal newPrice, int newID, int newSale, string newStock)
        public void InsertProduct(string newProductName, double newPrice, int newID)
        {
            /*_connection.Execute("INSERT INTO PRODUCTS (Name) VALUES (@ProductName, @ProductPrice, @ProductCategoryID, @ProductOnSale, @ProductStockLevel);",
            new { ProductName = newProductName, ProductPrice = newPrice, ProductCategoryID = newID, ProductOnSale = newSale, ProductStockLevel = newStock });*/
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@ProductName, @ProductPrice, @ProductCategoryID);",
            new { ProductName = newProductName, ProductPrice = newPrice, ProductCategoryID = newID });
        }
        public void UpdateProductName(int productID, string newName)
        {
            _connection.Execute("Update products SET name = @newName WHERE ProductID = @productID;",
                new {newName = newName, productID = productID });
        }
        public void DeleteProduct(int productID)
        {
            _connection.Execute("Delete from reviews where productID = @productID;",
                new { productID = productID });
            _connection.Execute("Delete from sales where productID = @productID;",
                new { productID = productID });
            _connection.Execute("Delete from products where productID = @productID;",
                new { productID = productID });
        }
    }
}
