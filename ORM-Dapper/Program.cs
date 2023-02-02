using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
//^^^^MUST HAVE USING DIRECTIVES^^^^

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            string userResponse = "";
            string[] userResponses = { "", "", "", "", "" };
            #region Configuration
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            //Console.WriteLine(connString);
            #endregion
            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DepartmentRepository(conn);

            #region Exercise I
            Console.WriteLine("Hello user, here are the current departments:");
            var departments = repo.GetAllDepartments();

            Print(departments);
            Onward();

            Console.WriteLine("Do you want to add a department (yes/no)?");
            userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new department?");
                userResponse = Console.ReadLine();
                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }
            #endregion

            #region Exercise II
            Onward();
            Console.WriteLine("Here are the current products:");
            var repo2 = new ProductRepository(conn);
            var prods = repo2.GetAllProducts();
            Print2(prods);


            Onward();
            Console.WriteLine("Do you want to add a product (yes/no)?");
            userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the NAME of your new product?");
                userResponses[0] = Console.ReadLine();
                Console.WriteLine("What is the PRICE of your new product?");
                userResponses[1] = Console.ReadLine();
                Console.WriteLine("What is the CATEGORY of your new product?");
                userResponses[2] = Console.ReadLine();
                /*Console.WriteLine("What is the ON-SALE status of your new product?");
                userResponses[3] = Console.ReadLine();
                Console.WriteLine("What is the STOCK of your new product?");
                userResponses[4] = Console.ReadLine();*/
                //repo2.InsertProduct(userResponses[0], decimal.Parse(userResponses[1]), int.Parse(userResponses[2]), int.Parse(userResponses[3]), userResponses[4]);
                repo2.InsertProduct(userResponses[0], double.Parse(userResponses[1]), int.Parse(userResponses[2]));
                Print2(repo2.GetAllProducts());
            }
            #endregion

            #region Bonus + Extra Bonus
            Onward();
            Console.WriteLine("Do you want to update a product's name (yes/no)?");
            userResponse = Console.ReadLine();
            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the ID of your new product?");
                userResponses[0] = Console.ReadLine();
                Console.WriteLine("What is the NEW NAME for your new product?");
                userResponses[1] = Console.ReadLine();
                repo2.UpdateProductName(int.Parse(userResponses[0]), userResponses[1]);
                Print2(repo2.GetAllProducts());
            }

            Onward();
            Console.WriteLine("Do you want to delete a product (yes/no)?");
            userResponse = Console.ReadLine();
            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the ID of your new product?");
                userResponse = Console.ReadLine();
                repo2.DeleteProduct(int.Parse(userResponse));
                Print2(repo2.GetAllProducts());
            }
            #endregion

            #region Finish
            Onward();
            Console.WriteLine("Work complete! Have a great day!");
            #endregion
        }
        private static void Print(IEnumerable<Department> departments)
        {
            foreach (var dept in departments)
            {
                Console.WriteLine($"#{dept.DepartmentID} {dept.Name}");
            }
        }
        private static void Print2(IEnumerable<Product> prods)
        {
            foreach (var dept in prods)
            {
                Console.WriteLine($"#{dept.ProductID} {dept.Name}: ${dept.Price}, Category #{dept.CategoryID}, {dept.OnSale} Sale, {dept.StockLevel} Stock");
            }
        }
        private static void Onward()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter Anything to Continue...");
            Console.ReadLine();
            Console.WriteLine("");
        }
    }
}
