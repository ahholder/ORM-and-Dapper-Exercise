using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ORM_Dapper
{
    public class DepartmentRepository : IDepartmentRepository
    {
        //Field (or local variable) for making queueries for database
        //Readonly prevents value or structure from being changed or defined after initialization
        private readonly IDbConnection _connection; //field

        //constructor
        public DepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM Departments;");
        }
        public void InsertDepartment(string newDepartmentName)
        {
            //Parameterized Query (ex. @departmentName) = something turned into a variable in SQL
            //Anonymous Type (ex. {departmentName = newDepartmentName}) = something SQL turns into whatever type is needed, with 'a' as default name
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
            new { departmentName = newDepartmentName });

            //Alt example below if you pass another parameter into InsertDepartment
            //public void InsertDepartment(string newDepartmentName, decimal newPrice, int newCategoryID, int newOnSale) {
            //...
            //new {departmentName = newDepartmentName, price = newPrice, categoryID = newCategoryID, onSale = newOnSale});
        }
    }
}
