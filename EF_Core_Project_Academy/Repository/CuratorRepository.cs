using Dapper;
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
using static Dapper.SqlMapper;

namespace EF_Core_Project_Academy.Repository
{
    public class CuratorRepository : IBaseRepository<Curator>
    {

        ////////// Dapper CRUD операции /////////////////////

        /*static IDbConnection CreateConn()
        {
            var cs = "Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(cs); // создаём подключение
            conn.Open();                      // открываем сразу (Dapper не открывает сам)
            return conn;                      // возвращаем открытое подключение
        }*/

        public int InsertDapper(Curator entity)
        {
            
            const string sql = @"   INSERT INTO Curators (curators_name, curators_surname)
                                    OUTPUT INSERTED.curators_id
                                    VALUES (@Name, @Surname);
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
                {
                    entity.Name,                   
                    entity.Surname
                });
                return newId;            
        }

        public Curator GetByIdDapper(int id)
        {
            const string sql = @" SELECT curators_id AS Id,
                                         curators_name,
                                         curators_surname
                                  FROM Curators
                                  WHERE curators_id = @Id;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.QuerySingleOrDefault<Curator>(sql, new { Id = id });
        }

        public int GetIdByNameDapper(string surname)
        {
            const string sql = @" SELECT curators_id 
                                  FROM Curators
                                  WHERE curators_surname = @Surname;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.ExecuteScalar<int>(sql, new { Surname = surname });
        }

        public int UpdateDapper(Curator entity)
        {
            const string sql = @"   UPDATE Curators 
                                    SET curators_name=@Name, curators_surname=@Surname
                                    OUTPUT INSERTED.curators_id
                                    WHERE curators_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Surname,
                entity.Id
            });
            return newId;
        }

        public IEnumerable<Curator> SelectDapper()
        {
            const string sql = @"   SELECT curators_id AS Id,
                                           curators_name AS Name,
                                           curators_surname AS Surname
                                    FROM Curators;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.Query<Curator>(sql);
        }

        public bool DeleteDapper(Curator entity)
        {
            const string sql = @"   DELETE
                                    FROM Curators 
                                    WHERE curators_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int res = conn.Execute(sql, new { Id = entity.Id});
            if (res == 1) return true;
            return false;
        }


        /////////////////////////////////////////////////////
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
