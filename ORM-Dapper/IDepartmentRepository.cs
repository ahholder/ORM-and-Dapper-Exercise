using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAllDepartments();
        //"Not tightly coupled" = string must be passed-in, but variable name does not need to be identical during implementation
        void InsertDepartment(string newDepartmentName);
    }
}
