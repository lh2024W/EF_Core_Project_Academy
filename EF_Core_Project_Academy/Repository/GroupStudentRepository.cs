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

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

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
