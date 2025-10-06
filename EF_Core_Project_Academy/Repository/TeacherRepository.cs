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
                var id = context.Teachers.Where(t => t.Id == entity.Id).Select(t => t.Id).FirstOrDefault();
                if (id > 0)
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
            if (entity is null) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                // Проверяем наличие дубля
                bool exists = context.Teachers.Any(t =>
                    t.Name == entity.Name &&
                    t.Surname == entity.Surname
                );

                if (exists)
                {
                    Console.WriteLine("Такой преподаватель уже есть!");
                    return 0;        // уже есть такой преподаватель, не добавляем
                }

                context.Teachers.Add(entity); //добавляем, возвращаем Id
                context.SaveChanges();

                return entity.Id;
                
            }
        }

        public IEnumerable<Teacher> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var allTeachers = context.Teachers.ToList();
                return allTeachers;
            }
        }

        public int Update(Teacher entity)
        {
            if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var t = context.Teachers.Find(entity.Id);
                if (t is null) return 0;

                // копируем нужные поля
                t.Name = entity.Name;
                t.Surname = entity.Surname;
                t.IsProfessor = entity.IsProfessor;
                t.Salary = entity.Salary;
                context.SaveChanges();

                return entity.Id;
            }
        }
    }
    
}
