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
    public class SubjectRepository : IBaseRepository<Subject>
    {


        ////////// Dapper CRUD операции /////////////////////

        /*static IDbConnection CreateConn()
        {
            var cs = "Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(cs); // создаём подключение
            conn.Open();                      // открываем сразу (Dapper не открывает сам)
            return conn;                      // возвращаем открытое подключение
        }*/

        public int InsertDapper(Subject entity)
        {

            const string sql = @"   INSERT INTO Subjects (subjects_name)
                                    OUTPUT INSERTED.subjects_id
                                    VALUES (@Name);
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name
            });
            return newId;
        }

        public Subject GetByIdDapper(int id)
        {
            const string sql = @" SELECT subjects_id AS Id,
                                         subjects_name
                                  FROM Subjects
                                  WHERE subjects_id = @Id;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.QuerySingleOrDefault<Subject>(sql, new { Id = id });
        }

        public int GetIdByNameDapper(string name)
        {
            const string sql = @" SELECT subjects_id 
                                  FROM Subjects
                                  WHERE subjects_name = @Name;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.ExecuteScalar<int>(sql, new { Name = name });
        }

        public int UpdateDapper(Subject entity)
        {
            const string sql = @"   UPDATE Subjects 
                                    SET subjects_name=@Name
                                    OUTPUT INSERTED.subjects_id
                                    WHERE subjects_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Id
            });
            return newId;
        }

        public IEnumerable<Subject> SelectDapper()
        {
            const string sql = @"   SELECT subjects_id AS Id,
                                           subjects_name AS Name
                                    FROM Subjects;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.Query<Subject>(sql);
        }

        public bool DeleteDapper(Subject entity)
        {
            const string sql = @"   DELETE
                                    FROM Subjects 
                                    WHERE subjects_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int res = conn.Execute(sql, new { Id = entity.Id });
            if (res == 1) return true;
            return false;
        }


        
        /// //////////////////////////////////////////////////////////////////////
        
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
