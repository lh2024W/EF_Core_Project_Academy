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

        ////////// Dapper CRUD операции /////////////////////

        /*static IDbConnection CreateConn()
        {
            var cs = "Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(cs); // создаём подключение
            conn.Open();                      // открываем сразу (Dapper не открывает сам)
            return conn;                      // возвращаем открытое подключение
        }*/

        public int InsertDapper(Teacher entity)
        {

            const string sql = @"   INSERT INTO Teachers (teachers_name, teachers_surname, teachers_salary, teachers_isProfessor)
                                    OUTPUT INSERTED.teachers_id
                                    VALUES (@Name, @Surname, @Salary, @IsProfessor);
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Surname,
                entity.Salary,
                entity.IsProfessor
            });
            return newId;
        }

        public Teacher GetByIdDapper(int id)
        {
            const string sql = @" SELECT teachers_id AS Id,
                                         teachers_name,
                                         teachers_salary,
                                         teachers_isProfessor
                                  FROM Teachers
                                  WHERE teachers_id = @Id;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.QuerySingleOrDefault<Teacher>(sql, new { Id = id });
        }

        public int GetIdByNameDapper(string surname)
        {
            const string sql = @" SELECT teachers_id 
                                  FROM Teachers
                                  WHERE teachers_surname = @Surname;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.ExecuteScalar<int>(sql, new { Surname = surname });
        }

        public int UpdateDapper(Teacher entity)
        {
            const string sql = @"   UPDATE Teachers 
                                    SET teachers_name=@Name, teachers_surname=@Surname, teachers_salary=@Salary, teachers_isProfessor=@IsProfessor
                                    OUTPUT INSERTED.teachers_id
                                    WHERE teachers_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Surname,
                entity.Salary,
                entity.IsProfessor,
                entity.Id
            });
            return newId;
        }

        public IEnumerable<Teacher> SelectDapper()
        {
            const string sql = @"   SELECT teachers_id AS Id,
                                           teachers_name AS Name,
                                           teachers_surname AS Surname,
                                           teachers_salary AS Salary,
                                           teachers_isProfessor AS IsProfessor
                                    FROM Teachers;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.Query<Teacher>(sql);
        }

        public bool DeleteDapper(Teacher entity)
        {
            const string sql = @"   DELETE
                                    FROM Teachers 
                                    WHERE teachers_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int res = conn.Execute(sql, new { Id = entity.Id });
            if (res == 1) return true;
            return false;
        }



        /// ///////////////////////////////////////////////////////////////////////////
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
