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

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

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
