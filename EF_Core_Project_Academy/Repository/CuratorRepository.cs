using EF_Core_Project_Academy.AcademyDBContext;
using EF_Core_Project_Academy.Interfaces;
using EF_Core_Project_Academy.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.Repository
{
    public class CuratorRepository : IBaseRepository<Curator>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        
        public bool Delete(Curator entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var id = context.Curators.Where(c => c.Id == entity.Id).Select(c => c.Id).FirstOrDefault();
                if (id > 0)
                {
                    context.Curators.Remove(entity);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public Curator GetById(int id)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var c = context.Curators.FirstOrDefault(c => c.Id == id);
                return c;
            }
        }

        public int GetIdByName(string surname)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var curId = context.Curators.Where(c => c.Surname == surname).Select(c => c.Id).FirstOrDefault();
                return curId;
            }
        }

        public int Insert(Curator entity)
        {
            if (entity is null) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                // Проверяем наличие дубля
                bool exists = context.Curators.Any(c =>
                    c.Name == entity.Name &&
                    c.Surname == entity.Surname
                );

                if (exists)
                {
                    Console.WriteLine("Такой куратор уже есть!");
                    return 0;        // уже есть такой куратор, не добавляем
                }

                context.Curators.Add(entity); //добавляем, возвращаем Id
                context.SaveChanges();

                return entity.Id;

            }
        }

        public IEnumerable<Curator> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var allCurators = context.Curators.ToList();
                return allCurators;
            }
        }

        public int Update(Curator entity)
        {
            if (entity is null || entity.Id <= 0)
                return 0;
            using (MyDBContext context = new MyDBContext())
            {
                var c = context.Curators.Find(entity.Id);
                if (c is null) return 0;

                // копируем нужные поля
                c.Name = entity.Name;
                c.Surname = entity.Surname;
                context.SaveChanges();

                return entity.Id;
            }
        }
    }
    
    
}
