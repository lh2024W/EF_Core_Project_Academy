using EF_Core_Project_Academy.AcademyDBContext;
using EF_Core_Project_Academy.Interfaces;
using EF_Core_Project_Academy.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Repository
{
    public class DepartmentRepository : IBaseRepository<Department>
    {
        
        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");
        
        
        public bool Delete(Department entity)
        {
            throw new NotImplementedException();
        }

        public Department GetById(int id)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var dep = context.Departments.FirstOrDefault(d => d.Id == id);
                return dep;
            }
        }

        public int GetIdByName(string name)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var depId = context.Departments.Where(d => d.Name == name).Select(d => d.Id).FirstOrDefault();
                return depId;
            }
        }

        public int Insert(Department entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
