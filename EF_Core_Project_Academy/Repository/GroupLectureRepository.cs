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
    public class GroupLectureRepository : IBaseRepository<GroupLecture>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(GroupLecture entity)
        {
            if (entity is null || entity.LectureId <= 0 || entity.GroupId <= 0)
                return false;
            using (MyDBContext context = new MyDBContext())
            {
                // ВАЖНО: убираем навигации, чтобы EF не пытался их вставлять/обновлять
                //Если есть еще прикрепленные сущности, их тоже нужно обнулить
                entity.Lecture = null;
                entity.Group = null;
                // прикрепляем ту же сущность к текущему контексту
                //Attach сообщает EF Core: «вот сущность, которая уже есть в базе — начни её отслеживать, но считай, что она не менялась».
                //Мы передаём объект с заполненным Id, но без остальных данных.
                //EF не будет посылать SELECT — просто берёт этот объект под контроль как Unchanged.
                context.GroupsLectures.Attach(entity);
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

        public GroupLecture GetById(int idGroup)
        {
            if (idGroup <= 0) return null;
            using (MyDBContext context = new MyDBContext())
            {
                var gl = context.GroupsLectures.Include(gl => gl.Group)
                                                .Include(gl => gl.Lecture.Teacher)
                                                .Include(gl => gl.Lecture.Subject)
                                                .FirstOrDefault(s => s.Id == idGroup);
                return gl;
            }
        }

        public int GetIdByName(string name)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var glId = context.GroupsLectures.Include(gl => gl.Group)
                                                  .Where(gl => gl.Group.Name == name)
                                                  .Select(gl => gl.Id)
                                                  .FirstOrDefault();
                return glId;
            }
        }

        public int Insert(GroupLecture entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                // лекция и группа должены существовать
                if (!context.Groups.Any(g => g.Id == entity.GroupId))
                {
                    Console.WriteLine("Такой группы нет!");
                    return 0;
                }

                if (!context.Lectures.Any(l => l.Id == entity.LectureId))
                {
                    Console.WriteLine("Такой лекции нет!");
                    return 0;
                }

                // ВАЖНО: не трогаем entity.Lecture и entity.Group, только FK
                entity.Lecture = null;
                entity.Group = null;

                context.GroupsLectures.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
        }

        public IEnumerable<GroupLecture> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var all = context.GroupsLectures.Include(gl => gl.Group)
                                                .Include(gl => gl.Lecture.Teacher)
                                                .Include(gl => gl.Lecture.Subject)
                                                .ToList();
                return all;
            }
        }

        public int Update(GroupLecture entity)
        {
            if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var gl = context.GroupsLectures.Find(entity.Id);
                if (gl is null) return 0;

                // Если группу меняем — она должна существовать
                if (entity.GroupId > 0 && !context.Groups.Any(g => g.Id == entity.GroupId))
                {
                    Console.WriteLine("Такой группы нет!");
                    return 0;
                }

                // Если лекцию меняем — она должна существовать
                if (entity.LectureId > 0 && !context.Lectures.Any(l => l.Id == entity.LectureId))
                {
                    Console.WriteLine("Такой лекции нет!");
                    return 0;
                }

                // копируем нужные поля

                if (entity.GroupId > 0) gl.GroupId = entity.GroupId;
                if (entity.LectureId > 0) gl.LectureId = entity.LectureId;

                context.SaveChanges();

                return entity.Id;
            }
        }
    }
    
}
