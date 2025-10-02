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
    public class SubjectRepository : IBaseRepository<Subject>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Subject GetById(int id)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var subj = context.Subjects.FirstOrDefault( s => s.Id == id);
                return subj;
            }
        }

        public int GetIdByName(string name)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var subjId = context.Subjects.Where(s => s.Name == name).Select(s => s.Id).FirstOrDefault();
                return subjId;
            }
        }

        public int Insert(Subject entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Subject> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
    
}
