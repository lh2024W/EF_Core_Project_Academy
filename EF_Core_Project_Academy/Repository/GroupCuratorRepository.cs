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
    public class GroupCuratorRepository : IBaseRepository<GroupCurator>
    {

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        public bool Delete(GroupCurator entity)
        {
            if (entity is null || entity.CuratorId <= 0 || entity.GroupId <= 0 )
                return false;
            using (MyDBContext context = new MyDBContext())
            {
                // ВАЖНО: убираем навигации, чтобы EF не пытался их вставлять/обновлять
                //Если есть еще прикрепленные сущности, их тоже нужно обнулить
                entity.Curator = null;
                entity.Group = null;
                // прикрепляем ту же сущность к текущему контексту
                //Attach сообщает EF Core: «вот сущность, которая уже есть в базе — начни её отслеживать, но считай, что она не менялась».
                //Мы передаём объект с заполненным Id, но без остальных данных.
                //EF не будет посылать SELECT — просто берёт этот объект под контроль как Unchanged.
                context.GroupsCurators.Attach(entity);
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

        public GroupCurator GetById(int id)
        {
            if (id <= 0) return null;
            using (MyDBContext context = new MyDBContext())
            {
                var gc = context.GroupsCurators.Include(gc => gc.Group)
                                                .Include(gc => gc.Curator)
                                                .FirstOrDefault(gc => gc.Id == id);
                return gc;
            }
        }

        public int GetIdByName(string nameGroup) 
        {
            using (MyDBContext context = new MyDBContext())
            {
                var gcId = context.GroupsCurators.Include(gc => gc.Group)
                                                  .Where(gc => gc.Group.Name == nameGroup)
                                                  .Select(gc => gc.Id)
                                                  .FirstOrDefault();
                return gcId;
            }
        }

        public int Insert(GroupCurator entity)
        {
            using (MyDBContext context = new MyDBContext())
            {
                // куратор и группа должены существовать
                if (!context.Groups.Any(g => g.Id == entity.GroupId))
                {
                    Console.WriteLine("Такой группы нет!");
                    return 0;
                }

                if (!context.Curators.Any(c => c.Id == entity.CuratorId))
                {
                    Console.WriteLine("Такого куратора нет!");
                    return 0;
                }
                                
                // ВАЖНО: не трогаем entity.Curator и entity.Group, только FK
                entity.Curator = null;
                entity.Group = null;

                context.GroupsCurators.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
        }

        public IEnumerable<GroupCurator> Select()
        {
            using (MyDBContext context = new MyDBContext())
            {
                var all = context.GroupsCurators.Include(gc => gc.Group)
                                                .Include(gc => gc.Curator)
                                                .ToList();
                return all;
            }
        }

        public int Update(GroupCurator entity)
        {
            if (entity is null || entity.Id <= 0) return 0;

            using (MyDBContext context = new MyDBContext())
            {
                var gc = context.GroupsCurators.Find(entity.Id);
                if (gc is null) return 0;

                // Если группу меняем — она должна существовать
                if (entity.GroupId > 0 && !context.Groups.Any(g => g.Id == entity.GroupId))
                {
                    Console.WriteLine("Такой группы нет!");
                    return 0;
                }

                // Если куратора меняем — он должен существовать
                if (entity.CuratorId > 0 && !context.Curators.Any(c => c.Id == entity.CuratorId))
                {
                    Console.WriteLine("Такого куратора нет!");
                    return 0;
                }

                // копируем нужные поля

                if (entity.GroupId > 0) gc.GroupId = entity.GroupId;
                if (entity.CuratorId > 0) gc.CuratorId = entity.CuratorId;

                context.SaveChanges();

                return entity.Id;
            }
        }
    }
    
}
