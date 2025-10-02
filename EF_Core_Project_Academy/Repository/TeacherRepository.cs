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
using static Dapper.SqlMapper;

namespace EF_Core_Project_Academy.Repository
{
    public class TeacherRepository : IBaseRepository<Teacher>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(Teacher entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                if (entity != null)
                {
                    context.Teachers.Remove(entity);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public Teacher GetById(int id)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var teach = context.Teachers.FirstOrDefault(t => t.Id == id);
                return teach;
            }
        }

        public int GetIdByName(string surname)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var teachId = context.Teachers.Where(t => t.Surname == surname).Select(t => t.Id).FirstOrDefault();
                return teachId;
            }
        }

        public int Insert(Teacher entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                if (entity != null)
                {
                    context.Teachers.Add(entity);
                    context.SaveChanges();
                    return entity.Id;
                }
                return 0;
            }
        }

        public IEnumerable<Teacher> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                context.Teachers.ToList();
                return context.Teachers;
            }
        }

        public int Update(Teacher entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                if (entity != null)
                {
                    context.Teachers.Update(entity);
                    context.SaveChanges();
                    return entity.Id;
                }
                return 0;
            }
        }
    }
    
}
