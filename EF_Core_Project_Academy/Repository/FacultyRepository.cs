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
            using (MyDBContext context = new MyDBContext())
            {
                var id = context.Faculties.Where(f => f.Id == entity.Id).Select(f => f.Id).FirstOrDefault();
                if (id > 0)
                {
                    context.Faculties.Remove(entity);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
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
            if (entity is null) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                // Проверяем наличие дубля
                bool exists = context.Faculties.Any(f =>
                    f.Name == entity.Name
                );

                if (exists)
                {
                    Console.WriteLine("Такой факультет уже есть!");
                    return 0;        // уже есть такой факультет, не добавляем
                }

                context.Faculties.Add(entity); //добавляем, возвращаем Id
                context.SaveChanges();

                return entity.Id;

            }
        }

        public IEnumerable<Faculty> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var allFaculties = context.Faculties.ToList();
                return allFaculties;
            }
        }

        public int Update(Faculty entity)
        {
            if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var f = context.Faculties.Find(entity.Id);
                if (f is null) return 0;

                // копируем нужные поля
                f.Name = entity.Name;

                context.SaveChanges();

                return entity.Id;
            }
        }
    }
}
