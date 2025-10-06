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
    public class LectureRepository : IBaseRepository<Lecture>
    {
        
        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");


        public bool Delete(Lecture entity)
        {
            if (entity is null || entity.SubjectId <= 0 || entity.TeacherId <= 0)
                return false;
            using (MyDBContext context = new MyDBContext())
            {
                // ВАЖНО: убираем навигации, чтобы EF не пытался их вставлять/обновлять
                //Если есть еще прикрепленные сущности, их тоже нужно обнулить
                entity.Subject = null;
                entity.Teacher = null;
                // прикрепляем ту же сущность к текущему контексту
                //Attach сообщает EF Core: «вот сущность, которая уже есть в базе — начни её отслеживать, но считай, что она не менялась».
                //Мы передаём объект с заполненным Id, но без остальных данных.
                //EF не будет посылать SELECT — просто берёт этот объект под контроль как Unchanged.
                context.Lectures.Attach(entity);
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

        public Lecture GetById(int idSubject)
        {
            if (idSubject <= 0) return null;
            using (MyDBContext context = new MyDBContext())
            {
                var l = context.Lectures.Include(l => l.Subject)
                                         .Include(l => l.Teacher)
                                         .FirstOrDefault(s => s.Id == idSubject);
                return l;
            }
        }

        public int GetIdByName(string nameSubject)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var lId = context.Lectures.Include(l => l.Subject)
                                                  .Where(l => l.Subject.Name == nameSubject)
                                                  .Select(l => l.Id)
                                                  .FirstOrDefault();
                return lId;
            }
        }

        public int GetId(string subjectName, string teacherSurname, DateOnly date)
        {
            using (MyDBContext context = new MyDBContext())
            {
                SubjectRepository sr = new SubjectRepository();
                TeacherRepository tr = new TeacherRepository();

                int subjectId = sr.GetIdByName(subjectName);
                int teacherId = tr.GetIdByName(teacherSurname);

                int lId = context.Lectures.Where(l => l.SubjectId == subjectId
                                                && l.TeacherId == teacherId
                                                && l.LectureDate == date)
                    .Select(l => l.Id)
                    .FirstOrDefault();   // вернёт 0 если ничего не найдено (для int)

                return lId;
            }
        }

        public int Insert(Lecture entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                // предмет и преподаватель должены существовать
                if (!context.Subjects.Any(s => s.Id == entity.SubjectId))
                {
                    Console.WriteLine("Такого предмета нет!");
                    return 0;
                }

                if (!context.Teachers.Any(t => t.Id == entity.TeacherId))
                {
                    Console.WriteLine("Такого преподавателя нет!");
                    return 0;
                }

                // ВАЖНО: не трогаем entity.Teacher и entity.Subject, только FK
                entity.Teacher = null;
                entity.Subject = null;

                context.Lectures.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
        }

        public IEnumerable<Lecture> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var all = context.Lectures.Include(l => l.Subject)
                                          .Include(l => l.Teacher)
                                          .ToList();
                return all;
            }
        }

        public int Update(Lecture entity)
        {
            if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var l = context.Lectures.Find(entity.Id);
                if (l is null) return 0;

                // Если предмет меняем — он должен существовать
                if (entity.SubjectId > 0 && !context.Subjects.Any(s => s.Id == entity.SubjectId))
                {
                    Console.WriteLine("Такого предмета нет!");
                    return 0;
                }

                // Если преподавателя меняем — он должен существовать
                if (entity.TeacherId > 0 && !context.Teachers.Any(t => t.Id == entity.TeacherId))
                {
                    Console.WriteLine("Такого преподавателя нет!");
                    return 0;
                }

                // копируем нужные поля
                l.LectureDate = entity.LectureDate;
                if (entity.SubjectId > 0) l.SubjectId = entity.SubjectId;
                if (entity.TeacherId > 0) l.TeacherId = entity.TeacherId;

                context.SaveChanges();

                return entity.Id;
            }
        }

    }
    
}
