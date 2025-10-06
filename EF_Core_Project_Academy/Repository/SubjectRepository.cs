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
            using (MyDBContext context = new MyDBContext())
            {
                var id = context.Subjects.Where(s => s.Id == entity.Id).Select(s => s.Id).FirstOrDefault();
                if (id > 0)
                {
                    context.Subjects.Remove(entity);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
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
            if (entity is null) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                // Проверяем наличие дубля
                bool exists = context.Subjects.Any(s =>
                    s.Name == entity.Name
                );

                if (exists)
                {
                    Console.WriteLine("Такой предмет уже есть!");
                    return 0;        // уже есть такой предмет, не добавляем
                }

                context.Subjects.Add(entity); //добавляем, возвращаем Id
                context.SaveChanges();

                return entity.Id;

            }
        }

        public IEnumerable<Subject> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var allSubjects = context.Subjects.ToList();
                return allSubjects;
            }
        }

        public int Update(Subject entity)
        {
            if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var s = context.Subjects.Find(entity.Id);
                if (s is null) return 0;

                // копируем нужные поля
                s.Name = entity.Name;

                context.SaveChanges();

                return entity.Id;
            }
        }
    }
    
}
