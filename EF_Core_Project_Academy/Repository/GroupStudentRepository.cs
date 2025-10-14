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
    public class GroupStudentRepository : IBaseRepository<GroupStudent>
    {

        ////////// Dapper CRUD операции /////////////////////

        /*static IDbConnection CreateConn()
        {
            var cs = "Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;";
            var conn = new SqlConnection(cs); // создаём подключение
            conn.Open();                      // открываем сразу (Dapper не открывает сам)
            return conn;                      // возвращаем открытое подключение
        }*/

        public int InsertDapper(GroupStudent entity)
        {
            const string sql = @"   INSERT INTO GroupsStudents (groupsStudents_groupId,
                                                                groupsStudents_studentId)
                                    OUTPUT INSERTED.groupsStudents_id
                                    VALUES (@GroupId, @StudentId);
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.GroupId,
                entity.StudentId,
                entity.Id
            });
            return newId;
        }

        public GroupStudent GetByIdDapper(int id)
        {
            const string sql = @" SELECT groupsStudents_id AS Id,
                                         groupsStudents_groupId,
                                         groupsStudents_studentId
                                  FROM GroupsStudents
                                  WHERE groupsStudents_id = @Id;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.QuerySingleOrDefault<GroupStudent>(sql, new { Id = id });
        }

        public int GetIdByNameDapper(string name)
        {
            const string sql = @" SELECT groupsStudents_id 
                                  FROM GroupsStudents
                                  JOIN Groups ON groupsStudents_groupId = groups_id
                                  WHERE groups_name = @Name;
                                ";

            using var conn = DbFactory.CreateConn();
            return conn.ExecuteScalar<int>(sql, new { Name = name });
        }

        public int UpdateDapper(GroupStudent entity)
        {
            const string sql = @"   UPDATE GroupsStudents 
                                    SET groupsStudents_groupId=@GroupId,
                                        groupsStudents_studentId=@StudentId
                                    OUTPUT INSERTED.groupsStudents_id
                                    WHERE groupsStudents_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int newId = conn.ExecuteScalar<int>(sql, new
            {
                entity.GroupId,
                entity.StudentId,
                entity.Id
            });
            return newId;
        }

        public IEnumerable<GroupStudent> SelectDapper()
        {
            const string sql = @"   SELECT groupsStudents_id AS Id,
                                           
                                           groups_id as Id,
                                           groups_name AS Name,
                                           groups_year AS Year,

                                           students_id as Id,
                                           students_name AS Name,
                                           students_surname AS Surname
                                    FROM GroupsStudents
                                    JOIN Groups ON groupsStudents_groupId = groups_id
                                    JOIN Students ON groupsStudents_studentId = students_id;
                                ";

            using var conn = DbFactory.CreateConn();
            // multi-mapping: сначала GroupsStudents, затем Group, затем Student,возвращаем GroupsStudents с присвоенным Group и Student
            var list = conn.Query<GroupStudent, Group, Student, GroupStudent>(
                sql,
                (grst, gr, st) => { grst.Group = gr; grst.Student = st; return grst; },
                splitOn: "Id" // <-- с какой колонки начинать маппить второй объект (Group и Student)
            );

            return list;
        }

        public bool DeleteDapper(GroupStudent entity)
        {
            const string sql = @"   DELETE
                                    FROM GroupsStudents 
                                    WHERE groupsStudents_id=@id;
                                ";

            using var conn = DbFactory.CreateConn();
            int res = conn.Execute(sql, new { Id = entity.Id });
            if (res == 1) return true;
            return false;
        }


        /// ///////////////////////////////////////////////////////////////////////////
        public bool Delete(GroupStudent entity)
        {
            if (entity is null || entity.StudentId <= 0 || entity.GroupId <= 0)
                return false;
            using (MyDBContext context = new MyDBContext())
            {
                // ВАЖНО: убираем навигации, чтобы EF не пытался их вставлять/обновлять
                //Если есть еще прикрепленные сущности, их тоже нужно обнулить
                entity.Student = null;
                entity.Group = null;
                // прикрепляем ту же сущность к текущему контексту
                //Attach сообщает EF Core: «вот сущность, которая уже есть в базе — начни её отслеживать, но считай, что она не менялась».
                //Мы передаём объект с заполненным Id, но без остальных данных.
                //EF не будет посылать SELECT — просто берёт этот объект под контроль как Unchanged.
                context.GroupsStudents.Attach(entity);
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

        public GroupStudent GetById(int idStudent)
        {
            if (idStudent <= 0) return null;
            using (MyDBContext context = new MyDBContext())
            {
                var gs = context.GroupsStudents.Include(gs => gs.Group)
                                                .Include(gs => gs.Student)
                                                .FirstOrDefault(s => s.Id == idStudent);
                return gs;
            }
        }

        public int GetIdByName(string surnameStudent)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var gsId = context.GroupsStudents.Include(gs => gs.Student)
                                                  .Where(gs => gs.Student.Surname == surnameStudent)
                                                  .Select(gs => gs.Id)
                                                  .FirstOrDefault();
                return gsId;
            }
        }

        public int Insert(GroupStudent entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                // студент и группа должены существовать
                if (!context.Groups.Any(g => g.Id == entity.GroupId))
                {
                    Console.WriteLine("Такой группы нет!");
                    return 0;
                }

                if (!context.Students.Any(s => s.Id == entity.StudentId))
                {
                    Console.WriteLine("Такого студента нет!");
                    return 0;
                }

                // ВАЖНО: не трогаем entity.Student и entity.Group, только FK
                entity.Student = null;
                entity.Group = null;

                context.GroupsStudents.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
        }

        public IEnumerable<GroupStudent> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var all = context.GroupsStudents.Include(gs => gs.Group)
                                                .Include(gs => gs.Student)
                                                .ToList();
                return all;
            }
        }

        public int Update(GroupStudent entity)
        {
            if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var gs = context.GroupsStudents.Find(entity.Id);
                if (gs is null) return 0;

                // Если группу меняем — она должна существовать
                if (entity.GroupId > 0 && !context.Groups.Any(g => g.Id == entity.GroupId))
                {
                    Console.WriteLine("Такой группы нет!");
                    return 0;
                }

                // Если студента меняем — он должен существовать
                if (entity.StudentId > 0 && !context.Students.Any(s => s.Id == entity.StudentId))
                {
                    Console.WriteLine("Такого студента нет!");
                    return 0;
                }

                // копируем нужные поля

                if (entity.GroupId > 0) gs.GroupId = entity.GroupId;
                if (entity.StudentId > 0) gs.StudentId = entity.StudentId;

                context.SaveChanges();

                return entity.Id;
            }
        }
    }
    
}
