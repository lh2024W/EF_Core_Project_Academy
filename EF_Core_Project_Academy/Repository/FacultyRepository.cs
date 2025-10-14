using Dapper;
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

        ////////// Dapper CRUD операции /////////////////////

        /*static IDbConnection CreateConn()
        {
            var cs = "Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(cs); // создаём подключение
            conn.Open();                      // открываем сразу (Dapper не открывает сам)
            return conn;                      // возвращаем открытое подключение
        }*/

        public int InsertDapper(Faculty entity)
        {

            const string sql = @"   INSERT INTO Faculties (faculties_name)
                                    OUTPUT INSERTED.faculties_id
                                    VALUES (@Name);
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name
            });
            return newId;
        }

        public Faculty GetByIdDapper(int id)
        {
            const string sql = @" SELECT faculties_id AS Id,
                                         faculties_name
                                  FROM Faculties
                                  WHERE faculties_id = @Id;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.QuerySingleOrDefault<Faculty>(sql, new { Id = id });
        }

        public int GetIdByNameDapper(string name)
        {
            const string sql = @" SELECT faculties_id 
                                  FROM Faculties
                                  WHERE faculties_name = @Name;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.ExecuteScalar<int>(sql, new { Name = name });
        }

        public int UpdateDapper(Faculty entity)
        {
            const string sql = @"   UPDATE Faculties 
                                    SET faculties_name=@Name
                                    OUTPUT INSERTED.faculties_id
                                    WHERE faculties_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Id
            });
            return newId;
        }

        public IEnumerable<Faculty> SelectDapper()
        {
            const string sql = @"   SELECT faculties_id AS Id,
                                           faculties_name AS Name
                                    FROM Faculties;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.Query<Faculty>(sql);
        }

        public bool DeleteDapper(Faculty entity)
        {
            const string sql = @"   DELETE
                                    FROM Faculties 
                                    WHERE faculties_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int res = conn.Execute(sql, new { Id = entity.Id });
            if (res == 1) return true;
            return false;
        }

        
        /// ////////////////////////////////////////////////////////////
        
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
