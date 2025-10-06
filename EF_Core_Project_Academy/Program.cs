using EF_Core_Project_Academy.AcademyDBContext;
using EF_Core_Project_Academy.Model;
using EF_Core_Project_Academy.Repository;
using System;
using static EF_Core_Project_Academy.AcademyDBContext.DbInit;

namespace EF_Core_Project_Academy
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var db = new MyDBContext();

            //db.Database.EnsureDeleted();//снести если не жалко данные
            //db.Database.EnsureCreated();//создать заново

            DbInit.Init(db);   // ← заполняем
            Console.WriteLine("Готово! Данные засеяны.");


            //////////////////    GroupLecture   ////////////////////

            //GroupLectureRepository glr = new GroupLectureRepository();
            //GroupRepository gr = new GroupRepository();
            //LectureRepository lr = new LectureRepository();


            /*GroupLecture gl = new GroupLecture()
            {
                GroupId = gr.GetIdByName("ПК-312"),
                LectureId = lr.GetId("Теория маркетинга", "Филатов", DateOnly.Parse("2025-09-19"))

            };
            Console.WriteLine(glr.Insert(gl));*/

            //int id = glr.GetIdByName("ПК-312");
            //Console.WriteLine(id);

            //Console.WriteLine(glr.GetIdByName("ПК-312"));

            /*GroupLecture gl = new GroupLecture();
            gl = glr.GetById(1);
            Console.WriteLine(gl.Group.Name + ' ' + gl.Lecture.Subject.Name + ' ' + gl.Lecture.Teacher.Surname + ' ' + gl.Lecture.Teacher.Name);
            */

            /*var all = glr.Select();
            foreach (var gl in all)
            {
                Console.WriteLine(gl.Group.Name + ' ' + gl.Lecture.Subject.Name + ' ' + gl.Lecture.Teacher.Surname + ' ' + gl.Lecture.Teacher.Name);
            }*/

            /*GroupLecture gl = new GroupLecture();
            gl = glr.GetById(1);
            gl.GroupId = 6;
            Console.WriteLine(glr.Update(gl));*/

            /*GroupLecture gl = new GroupLecture();
            gl = glr.GetById(1);
            gl.LectureId = 3;
            Console.WriteLine(glr.Update(gl));*/

            /*GroupLecture gl = new GroupLecture();
            gl = glr.GetById(1);
            Console.WriteLine(glr.Delete(gl));*/

            //////////////////    Lecture   ////////////////////

            //LectureRepository lr = new LectureRepository();
            //SubjectRepository sr = new SubjectRepository();
            //TeacherRepository tr = new TeacherRepository();

            /*Lecture l = new Lecture()
            {
                LectureDate = DateOnly.Parse("2025-09-23"),
                SubjectId = sr.GetIdByName("Математический анализ"),
                TeacherId = tr.GetIdByName("Бабич")
                
            };
            Console.WriteLine(lr.Insert(l));*/

            //int id = lr.GetIdByName("Математический анализ");
            //Console.WriteLine(id);

            //Console.WriteLine(lr.GetIdByName("Математический анализ"));

            /*Lecture l = new Lecture();
            l = lr.GetById(1);
            Console.WriteLine(l.LectureDate.ToString() + ' ' + l.Subject.Name + ' ' + l.Teacher.Surname + ' ' + l.Teacher.Name);*/

            /*var all = lr.Select();
            foreach (var l in all)
            {
                Console.WriteLine(l.LectureDate.ToString() + ' ' + l.Subject.Name + ' ' + l.Teacher.Surname + ' ' + l.Teacher.Name);
            }*/

            /*Lecture l = new Lecture();
            l = lr.GetById(1);
            l.SubjectId = 2;
            Console.WriteLine(lr.Update(l));*/

            /*Lecture l = new Lecture();
            l = lr.GetById(1);
            l.TeacherId = 3;
            Console.WriteLine(lr.Update(l));*/

            /*Lecture l = new Lecture();
            var id = lr.GetIdByName("Математический анализ");
            l = lr.GetById(id);
            l.LectureDate = DateOnly.Parse("2025-10-01");
            Console.WriteLine(lr.Update(l));*/

            /*Lecture l = new Lecture();
            l = lr.GetById(1);
            Console.WriteLine(lr.Delete(l));*/

            //////////////////    GroupStudent   ////////////////////

            //GroupStudentRepository gsr = new GroupStudentRepository();
            //GroupRepository gr = new GroupRepository();
            //StudentRepository sr = new StudentRepository();


            /*GroupStudent gs = new GroupStudent()
            {
                GroupId = gr.GetIdByName("ПК-312"),
                StudentId = sr.GetIdByName("Хачатрян")
                
            };
            Console.WriteLine(gsr.Insert(gs));*/

            //int id = gsr.GetIdByName("Хачатрян");
            //Console.WriteLine(id);

            //Console.WriteLine(gsr.GetIdByName("Хачатрян"));

            /*GroupStudent gs = new GroupStudent();
            gs = gsr.GetById(1);
            Console.WriteLine(gs.Group.Name + ' ' + gs.Student.Name + ' ' + gs.Student.Surname);*/

            /*var all = gsr.Select();
            foreach (var gs in all)
            {
                Console.WriteLine(gs.Group.Name + ' ' + gs.Student.Name + ' ' + gs.Student.Surname);
            }*/

            /*GroupStudent gs = new GroupStudent();
            gs = gsr.GetById(1);
            gs.GroupId = 6;
            Console.WriteLine(gsr.Update(gs));*/

            /*GroupStudent gs = new GroupStudent();
            gs = gsr.GetById(1);
            gs.StudentId = 3;
            Console.WriteLine(gsr.Update(gs));*/

            /*GroupStudent gs = new GroupStudent();
            gs = gsr.GetById(1);
            Console.WriteLine(gsr.Delete(gs));*/

            //////////////////    GroupCurator   ////////////////////

            //GroupCuratorRepository gcr = new GroupCuratorRepository();
            //GroupRepository gr = new GroupRepository();
            //CuratorRepository cr = new CuratorRepository();


            /*GroupCurator gc = new GroupCurator()
            {
                GroupId = gr.GetIdByName("ПК-312"),
                CuratorId = cr.GetIdByName("Собко")
                
            };
            Console.WriteLine(gcr.Insert(gc));*/

            //int id = gcr.GetIdByName("ПК-312");
            //Console.WriteLine(id);

            //Console.WriteLine(gcr.GetIdByName("ПК-312"));

            /*GroupCurator gc = new GroupCurator();
            gc = gcr.GetById(1);
            Console.WriteLine(gc.Group.Name + ' ' + gc.Curator.Name + ' ' + gc.Curator.Surname);*/

            /*var all = gcr.Select();
            foreach (var gc in all)
            {
                Console.WriteLine(gc.Group.Name + ' ' + gc.Curator.Name + ' ' + gc.Curator.Surname);
            }*/

            /*GroupCurator gc = new GroupCurator();
            gc = gcr.GetById(1);
            gc.GroupId = 6;
            Console.WriteLine(gcr.Update(gc));*/

            /*GroupCurator gc = new GroupCurator();
            gc = gcr.GetById(1);
            gc.CuratorId = 1;
            Console.WriteLine(gcr.Update(gc));*/

            /*GroupCurator gc = new GroupCurator();
            gc = gcr.GetById(1);
            Console.WriteLine(gcr.Delete(gc));*/

            //////////////////    Group   ////////////////////

            //GroupRepository gr = new GroupRepository();
            //DepartmentRepository dr = new DepartmentRepository();


            /*Group group = new Group()
            {
                Name = "HH-1",
                Year = 1,
                DepartmentId = dr.GetIdByName("Кафедра алгебры"),
            };
            Console.WriteLine(gr.Insert(group));*/

            //int id = gr.GetIdByName("HH-1");
            //Console.WriteLine(id);

            //Console.WriteLine(gr.GetIdByName("HH-1"));

            /*Group g = new Group();
            g = gr.GetById(1);
            Console.WriteLine(g.Name + ' ' + g.Year + ' ' + g.Department.Name);*/

            /*var groups = gr.Select();
            foreach (var g in groups)
            {
                Console.WriteLine(g.Name + ' ' + g.Year + ' ' + g.Department.Name);
            }*/

            /*Group g = new Group();
            g = gr.GetById(1);
            g.Name = "Ffна";
            Console.WriteLine(gr.Update(g));*/

            /*Group g = new Group();
            g = gr.GetById(1);
            g.DepartmentId = 6;
            Console.WriteLine(gr.Update(g));*/

            /*Group g = new Group();
            g = gr.GetById(1);
            bool deleted = gr.Delete(g);
            Console.WriteLine(deleted);*/

            /*Group g = new Group();
            g = gr.GetById(1);
            Console.WriteLine(gr.Delete(g));*/

            //////////////////    Department   ////////////////////

            //DepartmentRepository dr = new DepartmentRepository();
            //FacultyRepository fr = new FacultyRepository();

            //int id = fr.GetIdByName("Факультет економики");
            //Console.WriteLine(id);

            /*Department department = new Department()
            {
                Name = "Кафедра биологии",
                Financing = 125000,
                Building = 5,
                FacultyId = fr.GetIdByName("Факультет економики"),
            };
            Console.WriteLine(dr.Insert(department));*/

            /*Department d = new Department(); 
            d = dr.GetById(10);
            bool deleted = dr.Delete(d);
            Console.WriteLine(deleted);*/

            /*Department d = new Department();   
            d = dr.GetById(2);
            Console.WriteLine(dr.Delete(d));*/

            /*Department d = new Department();
            d = dr.GetById(2);
            Console.WriteLine(d.Name + ' ' + d.Financing + ' ' + d.Building + ' ' + d.Faculty.Name);*/

            //Console.WriteLine(dr.GetIdByName("Кафедра биологии"));

            /*var departments = dr.Select();
            foreach (var d in departments)
            {
                Console.WriteLine(d.Name + ' ' + d.Financing + ' ' + d.Building + ' ' + d.Faculty.Name);
            }*/

            /*Department d = new Department();
            d = dr.GetById(3);
            d.Name = "Ffна";
            Console.WriteLine(dr.Update(d));*/

            /*Department d = new Department();
            d = dr.GetById(2);
            d.FacultyId = 6;
            Console.WriteLine(dr.Update(d));*/


            //////////////   Curator   ////////////////
            //CuratorRepository cr = new CuratorRepository();

            //Curator curator = new Curator() { Name = "Инна", Surname = "Иванова" };

            /*Curator c = new Curator();
            c = db.Curators.First(c => c.Id == 6);
            bool deleted = cr.Delete(c);
            Console.WriteLine(deleted);*/

            //Curator c = new Curator();
            //c = cr.GetById(2);
            //Console.WriteLine(c.Name + ' ' + c.Surname);

            //Console.WriteLine(cr.GetIdByName("Собко"));

            //cr.Insert(curator);

            /*var curators = cr.Select();
            foreach (var c in curators)
            {
                Console.WriteLine(c.Name + ' ' + c.Surname);
            }*/

            /*Curator c = new Curator();
            c = db.Curators.First(c => c.Id == 7);
            c.Name = "Инна";
            int count = cr.Update(c);   
            Console.WriteLine($"Затронуто строк: {count}");
            */

            //////////////////    Student   ////////////////////

            //StudentRepository sr = new StudentRepository();

            //Student student = new Student() { Name = "Инна", Surname = "Иванова", Rating = 5 };

            /*Student s = new Student();
            s = db.Students.First(s => s.Id == 5);
            bool deleted = sr.Delete(s);
            Console.WriteLine(deleted);*/

            /*Student s = new Student();
            s = sr.GetById(5);
            Console.WriteLine(s.Name + ' ' + s.Surname + ' ' + s.Rating);*/

            //Console.WriteLine(sr.GetIdByName("Кучернюк"));

            //sr.Insert(student);

            /*var students = sr.Select();
            foreach (var s in students)
            {
                Console.WriteLine(s.Name + ' ' + s.Surname + ' ' + s.Rating);
            }*/

            /*Student s = new Student();
            s = db.Students.First(s => s.Id == 5);
            s.Name = "Яна";
            int count = sr.Update(s);   
            Console.WriteLine($"Затронуто строк: {count}");*/

            /////////////////////////   Faculty   //////////////////////////////

            //FacultyRepository fr = new FacultyRepository();

            //Faculty faculty = new Faculty() { Name = "Факультет журналистики"};

            /*Faculty f = new Faculty();
            f = db.Faculties.First(f => f.Id == 4);
            bool deleted = fr.Delete(f);
            Console.WriteLine(deleted);*/

            /*Faculty f = new Faculty();
            f = fr.GetById(2);
            Console.WriteLine(f.Name );*/

            //Console.WriteLine(fr.GetIdByName("Факультет журналистики"));

            //fr.Insert(faculty);

            /*var faculties = fr.Select();
            foreach (var f in faculties)
            {
                Console.WriteLine(f.Name);
            }*/

            /*Faculty f = new Faculty();
            f = db.Faculties.First(f => f.Id == 4);
            f.Name = "Факультет автомобилестроения";
            int count = fr.Update(f);   
            Console.WriteLine($"Затронуто строк: {count}");*/

            //////////////////    Subject    ////////////////////

            //SubjectRepository sr = new SubjectRepository();

            //Subject subject = new Subject() { Name = "Философия"};

            /*Subject s = new Subject();
            s = db.Subjects.First(s => s.Id == 7);
            bool deleted = sr.Delete(s);
            Console.WriteLine(deleted);*/

            /*Subject s = new Subject();
            s = sr.GetById(2);
            Console.WriteLine(s.Name);*/

            //Console.WriteLine(sr.GetIdByName("Философия"));

            //sr.Insert(subject);

            /*var subjects = sr.Select();
            foreach (var s in subjects)
            {
                Console.WriteLine(s.Name);
            }*/

            /*Subject s = new Subject();
            s = db.Subjects.First(s => s.Id == 7);
            s.Name = "История";
            int count = sr.Update(s);   
            Console.WriteLine($"Затронуто строк: {count}");*/

            //////////////////    Teacher   ////////////////////

            //TeacherRepository tr = new TeacherRepository();
            //Teacher teacher = new Teacher() { Name = "Александр", Surname = "Шехов", Salary = 12000, IsProfessor = false};
            //Console.WriteLine(tr.Insert(teacher));

            /*Teacher t = new Teacher();
            t = tr.GetById(5);
            bool deleted = tr.Delete(t);
            Console.WriteLine(deleted);*/

            /*Teacher t = new Teacher();
            t = tr.GetById(4);
            Console.WriteLine(tr.Delete(t));*/

            /*Teacher t = new Teacher();
            t = tr.GetById(5);
            Console.WriteLine(t.Name + ' ' + t.Surname + ' ' + t.Salary + ' ' + t.IsProfessor);*/

            //Console.WriteLine(tr.GetIdByName("Шехов"));

            //Console.WriteLine(tr.Insert(teacher));

            /*var teachers = tr.Select();
            foreach (var t in teachers)
            {
                Console.WriteLine(t.Name + ' ' + t.Surname + ' ' + t.Salary + ' ' + t.IsProfessor);
            }*/

            /*Teacher t = new Teacher();
            t = tr.GetById(5);
            t.Name = "Яна";
            int count = tr.Update(t);   
            Console.WriteLine($"Затронуто строк: {count}");*/

            /*Teacher t = new Teacher();
            t = tr.GetById(9);
            t.Name = "Яна";
            Console.WriteLine(tr.Update(t));*/




            Console.WriteLine("Готово!");
        }
    }
}
