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
    internal class StudentRepository : IBaseRepository<Student>
    {
        
        ////////// Dapper CRUD операции /////////////////////

        /*static IDbConnection CreateConn()
        {
            var cs = "Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(cs); // создаём подключение
            conn.Open();                      // открываем сразу (Dapper не открывает сам)
            return conn;                      // возвращаем открытое подключение
        }*/

        public int InsertDapper(Student entity)
        {

            const string sql = @"   INSERT INTO Students (students_name, students_surname, students_rating)
                                    OUTPUT INSERTED.students_id
                                    VALUES (@Name, @Surname, @Rating);
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Surname,
                entity.Rating
            });
            return newId;
        }

        public Student GetByIdDapper(int id)
        {
            const string sql = @" SELECT students_id AS Id,
                                         students_name,
                                         students_surname
                                  FROM Students
                                  WHERE students_id = @Id;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.QuerySingleOrDefault<Student>(sql, new { Id = id });
        }

        public int GetIdByNameDapper(string surname)
        {
            const string sql = @" SELECT students_id 
                                  FROM Students
                                  WHERE students_surname = @Surname;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.ExecuteScalar<int>(sql, new { Surname = surname });
        }

        public int UpdateDapper(Student entity)
        {
            const string sql = @"   UPDATE Students 
                                    SET students_name=@Name, students_surname=@Surname, students_rating=@Rating
                                    OUTPUT INSERTED.students_id
                                    WHERE students_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Surname,
                entity.Rating,
                entity.Id
            });
            return newId;
        }

        public IEnumerable<Student> SelectDapper()
        {
            const string sql = @"   SELECT students_id AS Id,
                                           students_name AS Name,
                                           students_surname AS Surname,
                                           students_rating AS Rating
                                    FROM Students;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.Query<Student>(sql);
        }

        public bool DeleteDapper(Student entity)
        {
            const string sql = @"   DELETE
                                    FROM Students 
                                    WHERE students_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int res = conn.Execute(sql, new { Id = entity.Id });
            if (res == 1) return true;
            return false;
        }


        
        /// ///////////////////////////////////////////////////////////////////////////
        
        public bool Delete(Student entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var id = context.Students.Where(s => s.Id == entity.Id).Select(s => s.Id).FirstOrDefault();
                if (id > 0)
                {
                    context.Students.Remove(entity);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public Student GetById(int id)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var st = context.Students.FirstOrDefault(s => s.Id == id);
                return st;
            }
        }

        public int GetIdByName(string surname)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var stId = context.Students.Where(s => s.Surname == surname).Select(s => s.Id).FirstOrDefault();
                return stId;
            }
        }

        public int Insert(Student entity)
        {
            if (entity is null) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                // Проверяем наличие дубля
                bool exists = context.Students.Any(s =>
                    s.Name == entity.Name &&
                    s.Surname == entity.Surname
                );

                if (exists)
                {
                    Console.WriteLine("Такой студент уже есть!");
                    return 0;        // уже есть такой студент, не добавляем
                }

                context.Students.Add(entity); //добавляем, возвращаем Id
                context.SaveChanges();

                return entity.Id;

            }
        }

        public IEnumerable<Student> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var allStudents = context.Students.ToList();
                return allStudents;
            }
        }

        public int Update(Student entity)
        {
            if (entity is null || entity.Id <= 0)  return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var s = context.Students.Find(entity.Id);
                if (s is null) return 0;

                // копируем нужные поля
                s.Name = entity.Name;
                s.Surname = entity.Surname;
                s.Rating = entity.Rating;

                context.SaveChanges();

                return entity.Id;
            }
        }
    }
    
}
