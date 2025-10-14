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

namespace EF_Core_Project_Academy.Repository
{
    public class DepartmentRepository : IBaseRepository<Department>
    {

        ////////// Dapper CRUD операции /////////////////////

        /*static IDbConnection CreateConn()
        {
            var cs = "Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(cs); // создаём подключение
            conn.Open();                      // открываем сразу (Dapper не открывает сам)
            return conn;                      // возвращаем открытое подключение
        }*/

        public int InsertDapper(Department entity)
        {

            const string sql = @"   INSERT INTO Departments (departments_name, 
                                                            departments_building, 
                                                            departments_financing, 
                                                            departments_facultyId)
                                    OUTPUT INSERTED.departments_id
                                    VALUES (@Name, @Building, @Financing, @FacultyId);
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Financing,
                entity.Building,
                entity.FacultyId,
                entity.Id
            });
            return newId;
        }

        public Department GetByIdDapper(int id)
        {
            const string sql = @" SELECT departments_id AS Id,
                                         departments_name,
                                         departments_financing,
                                         departments_building,
                                         departments_facultyId
                                  FROM Departments
                                  WHERE departments_id = @Id;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.QuerySingleOrDefault<Department>(sql, new { Id = id });
        }

        public int GetIdByNameDapper(string name)
        {
            const string sql = @" SELECT departments_id 
                                  FROM Departments
                                  WHERE departments_name = @Name;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.ExecuteScalar<int>(sql, new { Name = name });
        }

        public int UpdateDapper(Department entity)
        {
            const string sql = @"   UPDATE Departments 
                                    SET departments_name=@Name, 
                                        departments_financing=@Financing, 
                                        departments_building=@Building, 
                                        departments_facultyId=@FacultyId
                                    OUTPUT INSERTED.departments_id
                                    WHERE departments_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Financing,
                entity.Building,
                entity.FacultyId,
                entity.Id
            });
            return newId;
        }

        public IEnumerable<Department> SelectDapper()
        {
            const string sql = @"   SELECT departments_id AS Id,
                                           departments_name AS Name,
                                           departments_financing AS Financing,
                                           departments_building AS Building,

                                           faculties_id as Id,
                                           faculties_name AS Name
                                    FROM Departments
                                    JOIN Faculties ON departments_facultyId = faculties_id;
                                ";

            using var conn = DbFactory.CreateConn();
            // multi-mapping: сначала Department, затем Faculty, возвращаем Department с присвоенным Faculty
            var list = conn.Query<Department, Faculty, Department>(
                sql,
                (dep, fac) => { dep.Faculty = fac; return dep; },
                splitOn: "Id" // <-- с какой колонки начинать маппить второй объект (Faculty)
            );

            return list;
        }

        public bool DeleteDapper(Department entity)
        {
            const string sql = @"   DELETE
                                    FROM Departments 
                                    WHERE departments_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int res = conn.Execute(sql, new { Id = entity.Id });
            if (res == 1) return true;
            return false;
        }



        /// ///////////////////////////////////////////////////////////////////////////

        public bool Delete(Department entity)
        {
            if (entity is null || entity.FacultyId <= 0)
                return false;
            using (MyDBContext context = new MyDBContext())
            {
                // ВАЖНО: убираем навигации, чтобы EF не пытался их вставлять/обновлять
                //Если есть еще прикрепленные сущности, их тоже нужно обнулить
                entity.Faculty = null;
                // прикрепляем ту же сущность к текущему контексту
                //Attach сообщает EF Core: «вот сущность, которая уже есть в базе — начни её отслеживать, но считай, что она не менялась».
                //Мы передаём объект с заполненным Id, но без остальных данных.
                //EF не будет посылать SELECT — просто берёт этот объект под контроль как Unchanged.
                context.Departments.Attach(entity);
                //Меняем состояние отслеживаемой сущности на Deleted.
                //EF теперь знает: «при SaveChanges() надо выполнить DELETE по ключу этого объекта».
                //Когда вызываем SaveChanges(), EF генерирует SQL - запрос
                context.Entry(entity).State = EntityState.Deleted;

                try
                {
                    return context.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("Уже удалено или не найдено!"); 
                    return false;
                }
            }
        }

        public Department GetById(int id)
        {
            if (id <= 0) return null;
            using (MyDBContext context = new MyDBContext())
            {
                return context.Departments.Include(d => d.Faculty).FirstOrDefault(d => d.Id == id);
            }
        }

        public int GetIdByName(string name)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var depId = context.Departments.Where(d => d.Name == name).Select(d => d.Id).FirstOrDefault();
                return depId;
            }
        }

        public int Insert(Department entity)
        {
            if (entity is null || entity.FacultyId <= 0)
                return 0;

            using (MyDBContext context = new MyDBContext())
            {
                // факультет должен существовать
                if (!context.Faculties.Any(f => f.Id == entity.FacultyId))
                {
                    Console.WriteLine("Такого факультета нет!");
                    return 0;
                }    

                // защита от дублей (на одном факультете не две одинаковые кафедры)
                bool duplicate = context.Departments.Any(d =>
                                d.FacultyId == entity.FacultyId &&
                                d.Name == entity.Name);
                if (duplicate)
                {
                    Console.WriteLine("Такая кафедра уже есть или такая кафедра на таком факультете уже есть!");
                    return 0;
                }

                // ВАЖНО: не трогаем entity.Faculty, только FK
                entity.Faculty = null;

                context.Departments.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
        }

        public IEnumerable<Department> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var allDepartments = context.Departments.Include(d => d.Faculty).ToList();
                return allDepartments;
            }
        }

        public int Update(Department entity)
        {
           if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var d = context.Departments.Find(entity.Id);
                if (d is null) return 0;

                // Если факультет меняем — он должен существовать
                if (entity.FacultyId > 0 && !context.Faculties.Any(f => f.Id == entity.FacultyId))
                {
                    Console.WriteLine("Такого факультета нет!");
                    return 0;
                }
                    
                //Проверка дубля: то же имя на том же факультете (кроме текущей записи)          
                int id = entity.FacultyId > 0 ? entity.FacultyId : d.FacultyId;

                bool duplicate = context.Departments.Any(d => d.Id != entity.Id
                                                           && d.FacultyId == id
                                                           && d.Name == entity.Name);
                if (duplicate)
                {
                    Console.WriteLine("Такая кафедра уже есть или такая кафедра на таком факультете уже есть!");
                    return 0;
                }
                // копируем нужные поля
                d.Name = entity.Name;
                d.Financing = entity.Financing;
                d.Building = entity.Building;
                if (entity.FacultyId > 0) d.FacultyId = entity.FacultyId;

                context.SaveChanges();

                return entity.Id;
            }
        }
    }
}
