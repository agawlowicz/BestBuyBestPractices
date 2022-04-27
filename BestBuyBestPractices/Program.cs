using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DapperDepartmentRepository(conn);
            var departments = repo.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }

            Console.WriteLine("Type a new Department name");

            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);

            departments = repo.GetAllDepartments();

            foreach(var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }

            var productRepo = new DapperProductRepository(conn);
            var products = productRepo.GetAllProducts();

            foreach(var prod in products)
            {
                Console.WriteLine($"{prod.Name} {prod.Price} {prod.CategoryID}");
            }

            Console.WriteLine("Type a new Product name");

            var newProductName = Console.ReadLine();

            Console.WriteLine("Type price");
            var newProductPrice = Console.ReadLine();

            Console.WriteLine("Type category ID");
            var newCategoryID = Console.ReadLine();

            productRepo.CreateProduct(newProductName, double.Parse(newProductPrice), int.Parse(newCategoryID));

            products = productRepo.GetAllProducts();

            foreach(var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
