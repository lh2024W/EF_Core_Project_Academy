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
    public class GroupRepository : IBaseRepository<Group>
    {
                        ////////// Dapper CRUD операции /////////////////////

        /*static IDbConnection CreateConn()
        {
            var cs = "Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(cs); // создаём подключение
            conn.Open();                      // открываем сразу (Dapper не открывает сам)
            return conn;                      // возвращаем открытое подключение
        }*/

        public int InsertDapper(Group entity)
        {
            const string sql = @"   INSERT INTO Groups (groups_name,
                                                        groups_year, 
                                                        groups_departmentId)
                                    OUTPUT INSERTED.groups_id
                                    VALUES (@Name, @Year, @DepartmentId);
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Year,
                entity.DepartmentId,
                entity.Id
            });
            return newId;
        }

        public Group GetByIdDapper(int id)
        {
            const string sql = @" SELECT groups_id AS Id,
                                         groups_name,
                                         groups_year,
                                         groups_departmentId
                                  FROM Groups
                                  WHERE groups_id = @Id;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.QuerySingleOrDefault<Group>(sql, new { Id = id });
        }

        public int GetIdByNameDapper(string name)
        {
            const string sql = @" SELECT groups_id 
                                  FROM Groups
                                  WHERE groups_name = @Name;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.ExecuteScalar<int>(sql, new { Name = name });
        }

        public int UpdateDapper(Group entity)
        {
            const string sql = @"   UPDATE Groups 
                                    SET groups_name=@Name, 
                                        groups_year=@Year, 
                                        groups_departmentId=@DepartmentId
                                    OUTPUT INSERTED.groups_id
                                    WHERE groups_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.Name,
                entity.Year,
                entity.DepartmentId,
                entity.Id
            });
            return newId;
        }

        public IEnumerable<Group> SelectDapper()
        {
            const string sql = @"   SELECT groups_id AS Id,
                                           groups_name AS Name,
                                           groups_year AS Year,
                                           
                                           departments_id as Id,
                                           departments_name AS Name
                                    FROM Groups
                                    JOIN Departments ON groups_departmentId = departments_id;
                                ";

            using var conn = DbFactory.CreateConn();
            // multi-mapping: сначала Group, затем Department, возвращаем Group с присвоенным Department
            var list = conn.Query<Group, Department, Group>(
                sql,
                (gr, dep) => { gr.Department = dep; return gr; },
                splitOn: "Id" // <-- с какой колонки начинать маппить второй объект (Department)
            );

            return list;
        }

        public bool DeleteDapper(Group entity)
        {
            const string sql = @"   DELETE
                                    FROM Groups 
                                    WHERE groups_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int res = conn.Execute(sql, new { Id = entity.Id });
            if (res == 1) return true;
            return false;
        }


        /// ///////////////////////////////////////////////////////////////////////////
        
        public bool Delete(Group entity)
        {
            if (entity is null || entity.DepartmentId <= 0)
                return false;
            using (MyDBContext context = new MyDBContext())
            {
                // ВАЖНО: убираем навигации, чтобы EF не пытался их вставлять/обновлять
                //Если есть еще прикрепленные сущности, их тоже нужно обнулить
                entity.Department = null;
                // прикрепляем ту же сущность к текущему контексту
                //Attach сообщает EF Core: «вот сущность, которая уже есть в базе — начни её отслеживать, но считай, что она не менялась».
                //Мы передаём объект с заполненным Id, но без остальных данных.
                //EF не будет посылать SELECT — просто берёт этот объект под контроль как Unchanged.
                context.Groups.Attach(entity);
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

        public Group GetById(int id)
        {
            if (id <= 0) return null;
            using (MyDBContext context = new MyDBContext())
            {
                var gr = context.Groups.Include(g => g.Department).FirstOrDefault(g => g.Id == id);
                return gr;
            }
        }

        public int GetIdByName(string name)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var grId = context.Groups.Where(g => g.Name == name).Select(g => g.Id).FirstOrDefault();
                return grId;
            }
        }

        public int Insert(Group entity)
        {
            if (entity is null || entity.DepartmentId <= 0)
                return 0;

            using (MyDBContext context = new MyDBContext())
            {
                // кафедра должена существовать
                if (!context.Departments.Any(d => d.Id == entity.DepartmentId))
                {
                    Console.WriteLine("Такой кафедры нет!");
                    return 0;
                }

                // защита от дублей (на одной кафедре не две одинаковые группы)
                bool duplicate = context.Groups.Any(g =>
                                g.DepartmentId == entity.DepartmentId &&
                                g.Name == entity.Name);
                if (duplicate)
                {
                    Console.WriteLine("Такая группа уже есть или такая группа на такой кафедре уже есть!");
                    return 0;
                }

                // ВАЖНО: не трогаем entity.Department, только FK
                entity.Department = null;

                context.Groups.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
        }

        public IEnumerable<Group> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var allGroups = context.Groups.Include(g => g.Department).ToList();
                return allGroups;
            }
        }

        public int Update(Group entity)
        {
            if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var g = context.Groups.Find(entity.Id);
                if (g is null) return 0;

                // Если кафедру меняем — она должна существовать
                if (entity.DepartmentId > 0 && !context.Departments.Any(d => d.Id == entity.DepartmentId))
                {
                    Console.WriteLine("Такой кафедры нет!");
                    return 0;
                }

                //Проверка дубля: то же имя на той же кафедре (кроме текущей записи)          
                int id = entity.DepartmentId > 0 ? entity.DepartmentId : g.DepartmentId;

                bool duplicate = context.Groups.Any(g => g.Id != entity.Id
                                                           && g.DepartmentId == id
                                                           && g.Name == entity.Name);
                if (duplicate)
                {
                    Console.WriteLine("Такая группа уже есть или такая группа на такой кафедре уже есть!");
                    return 0;
                }
                // копируем нужные поля
                g.Name = entity.Name;
                g.Year = entity.Year;
                if (entity.DepartmentId > 0) g.DepartmentId = entity.DepartmentId;

                context.SaveChanges();

                return entity.Id;
            }
        }
    }
    
}
