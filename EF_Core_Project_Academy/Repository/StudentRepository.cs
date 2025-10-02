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
    internal class StudentRepository : IBaseRepository<Student>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(Student entity)
        {
            throw new NotImplementedException();
        }

        public Student GetById(int id)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var st = context.Students.FirstOrDefault(s => s.Id == id);
                return st;
            }
        }

        public int GetIdByName(string name)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var stId = context.Students.Where(s => s.Name == name).Select(s => s.Id).FirstOrDefault();
                return stId;
            }
        }

        public int Insert(Student entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
    
}
