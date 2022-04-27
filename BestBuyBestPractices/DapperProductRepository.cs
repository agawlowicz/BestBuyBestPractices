using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Dapper;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@productName, @productPrice, @productCategory);",
             new { productName = name, productPrice = price, productCategory = categoryID });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { ProductID = productID });
            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { ProductID = productID });
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { ProductID = productID });

        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }

        public void UpdateProduct(int productID, double updatedPrice)
        {
            _connection.Execute("UPDATE products SET Price = @updatedPrice WHERE ProductID = @productID;",
                new {productID = productID, updatedPrice = updatedPrice});
            Console.WriteLine("Product Updated");
            Thread.Sleep(3000);
        }
    }
}
