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
    public class FacultyRepository : IBaseRepository<Faculty>
    {
       IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(Faculty entity)
        {
            throw new NotImplementedException();
        }

        public Faculty GetById(int id)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var fuc = context.Faculties.FirstOrDefault(f => f.Id == id);
                return fuc;
            }
        }

        public int GetIdByName(string name)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var fucId = context.Faculties.Where(f => f.Name == name).Select(f => f.Id).FirstOrDefault();
                return fucId;
            }
        }

        public int Insert(Faculty entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Faculty> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Faculty entity)
        {
            throw new NotImplementedException();
        }
    }
}
