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
        
        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");
        
        
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
