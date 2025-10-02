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
        
        public LectureRepository(MyDBContext context) { }

        IDbConnection connection = new SqlConnection(@"Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");


        public bool Delete(Lecture entity)
        {
            throw new NotImplementedException();
        }

        public Lecture GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetIdByName(string name)
        {
            throw new NotImplementedException();
        }

        public int GetId(string subjectName, string teacherSurname, DateOnly date)
        {
            using (MyDBContext context = new MyDBContext())
            {
                var sr = new SubjectRepository();
                var tr = new TeacherRepository();

                var subjectId = sr.GetIdByName(subjectName);
                var teacherId = tr.GetIdByName(teacherSurname);

                var lecId = context.Lectures
                    .Where(l => l.SubjectId == subjectId
                             && l.TeacherId == teacherId
                             && l.LectureDate == date)
                    .Select(l => l.Id)
                    .FirstOrDefault();   // вернёт 0 если ничего не найдено (для int)

                return lecId;
            }
        }

        public int Insert(Lecture entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lecture> Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Lecture entity)
        {
            throw new NotImplementedException();
        }

    }
    
}
