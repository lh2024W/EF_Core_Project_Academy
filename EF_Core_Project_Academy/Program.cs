using EF_Core_Project_Academy.AcademyDBContext;
using static EF_Core_Project_Academy.AcademyDBContext.DbInit;

namespace EF_Core_Project_Academy
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var db = new MyDBContext();
            
            db.Database.EnsureDeleted();//снести если не жалко данные
            db.Database.EnsureCreated();//создать заново

            DbInit.Init(db);   // ← заполняем
            Console.WriteLine("Готово! Данные засеяны.");
        }
    }
}
