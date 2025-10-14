using EF_Core_Project_Academy.Model;
using EF_Core_Project_Academy.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.AcademyDBContext
{
    public static class DbInit
    {
        public static void Init(MyDBContext db)
        {
            ////////////////// заполняем родительские таблицы //////////////////////////////

            if (!db.Curators.Any())
            {
                db.Curators.AddRange(new Curator[]
                {
                    new Curator { Name = "Петр", Surname = "Воробьев"},
                    new Curator { Name = "Инга", Surname = "Иванова"},
                    new Curator { Name = "Наталия", Surname = "Собко"},
                    new Curator { Name = "Ольга", Surname = "Петрова"},
                    new Curator { Name = "Сергей", Surname = "Кабанов"}
                });
                db.SaveChanges();
            }

            if (!db.Faculties.Any())
            {
                db.Faculties.AddRange(new Faculty[]
                {
                   new Faculty { Name = "Факультет математики"},
                   new Faculty { Name = "Факультет компьютерных наук"},
                   new Faculty { Name = "Факультет економики"}
                });
                db.SaveChanges();
            }

            if (!db.Students.Any())
            {
                db.Students.AddRange(new Student[]
                {
                    new Student { Name = "Лилия", Rating = 3, Surname = "Хачатрян"},
                    new Student { Name = "Олег", Rating = 5, Surname = "Шипков"},
                    new Student { Name = "Сергей", Rating = 4, Surname = "Михайлов"},
                    new Student { Name = "Ольга", Rating = 3, Surname = "Санкина"},
                    new Student { Name = "Ирина", Rating = 5, Surname = "Корса"},
                    new Student { Name = "Ирина", Rating = 5, Surname = "Блажко"},
                    new Student { Name = "Анна", Rating = 1, Surname = "Глущенко"},
                    new Student { Name = "Иван", Rating = 5, Surname = "Петров"},
                    new Student { Name = "Петр", Rating = 3, Surname = "Иванов"},
                    new Student { Name = "Алексей", Rating = 3, Surname = "Кучернюк"},
                    new Student { Name = "Дмитрий", Rating = 5, Surname = "Кичуга"}

                });
                db.SaveChanges();
            }

            if (!db.Subjects.Any())
            {
                db.Subjects.AddRange(new Subject[]
                {
                    new Subject { Name = "Математический анализ"},
                    new Subject { Name = "Макроекономика"},
                    new Subject { Name = "Микроекономика"},
                    new Subject { Name = "Кибербезопасность"},
                    new Subject { Name = "Теория маркетинга"}
                });
                db.SaveChanges();
            }

            if (!db.Teachers.Any())
            {
                db.Teachers.AddRange(new Teacher[]
                {
                    new Teacher { IsProfessor = true , Name = "Светлана", Salary = 15000, Surname = "Бабич"},
                    new Teacher { IsProfessor = false , Name = "Лидия", Salary = 10000, Surname = "Грач"},
                    new Teacher { IsProfessor = false , Name = "Игорь", Salary = 10000, Surname = "Федоров"},
                    new Teacher { IsProfessor = false , Name = "Александр", Salary = 10000, Surname = "Филатов"},
                    new Teacher { IsProfessor = true , Name = "Александр", Salary = 15000, Surname = "Шехов"}
                });
                db.SaveChanges();
            }

            /////////////////////////////// заполняем дочерние таблицы //////////////////////////////////

            if (!db.Departments.Any())
            {
                DepartmentRepository dr = new DepartmentRepository();
                FacultyRepository fr = new FacultyRepository();

                Department department1 = new Department()
                {
                    Building = 1,
                    Financing = 100000,
                    Name = "Кафедра алгебры",
                    FacultyId = fr.GetIdByName("Факультет математики"),
                };
                dr.Insert(department1);

                Department department2 = new Department()
                {
                    Building = 1,
                    Financing = 110000,
                    Name = "Кафедра мат. анализа",
                    FacultyId = fr.GetIdByName("Факультет математики"),
                };
                dr.Insert(department2);

                Department department3 = new Department()
                {
                    Building = 1,
                    Financing = 80000,
                    Name = "Кафедра дифф. уравнений",
                    FacultyId = fr.GetIdByName("Факультет математики"),
                };
                dr.Insert(department3);

                Department department4 = new Department()
                {
                    Building = 3,
                    Financing = 200000,
                    Name = "Кафедра программной инженерии",
                    FacultyId = fr.GetIdByName("Факультет компьютерных наук"),
                };
                dr.Insert(department4);

                Department department5 = new Department()
                {
                    Building = 3,
                    Financing = 250000,
                    Name = "Кафедра искусственного интеллекта",
                    FacultyId = fr.GetIdByName("Факультет компьютерных наук"),
                };
                dr.Insert(department5);

                Department department6 = new Department()
                {
                    Building = 3,
                    Financing = 190000,
                    Name = "Кафедра кибербезопасности",
                    FacultyId = fr.GetIdByName("Факультет компьютерных наук"),
                };
                dr.Insert(department6);

                Department department7 = new Department()
                {
                    Building = 2,
                    Financing = 50000,
                    Name = "Кафедра финансов",
                    FacultyId = fr.GetIdByName("Факультет економики"),
                };
                dr.Insert(department7);

                Department department8 = new Department()
                {
                    Building = 2,
                    Financing = 60000,
                    Name = "Кафедра бухгалтерского учета",
                    FacultyId = fr.GetIdByName("Факультет економики"),
                };
                dr.Insert(department8);

                Department department9 = new Department()
                {
                    Building = 2,
                    Financing = 50000,
                    Name = "Кафедра маркетинга",
                    FacultyId = fr.GetIdByName("Факультет економики"),
                };
                dr.Insert(department9);

            }

            if (!db.Groups.Any())
            {
                DepartmentRepository dr = new DepartmentRepository();
                GroupRepository gr = new GroupRepository();

                Group group1 = new Group()
                {
                    Name = "ПК-312",
                    Year = 1,
                    DepartmentId = dr.GetIdByName("Кафедра маркетинга")
                };
                gr.Insert(group1);

                Group group2 = new Group()
                {
                    Name = "ПК-211",
                    Year = 2,
                    DepartmentId = dr.GetIdByName("Кафедра маркетинга")
                };
                gr.Insert(group2);

                Group group3 = new Group()
                {
                    Name = "ПК-100",
                    Year = 3,
                    DepartmentId = dr.GetIdByName("Кафедра маркетинга")
                };
                gr.Insert(group3);

                Group group4 = new Group()
                {
                    Name = "ПК-80",
                    Year = 4,
                    DepartmentId = dr.GetIdByName("Кафедра маркетинга")
                };
                gr.Insert(group4);

                Group group5 = new Group()
                {
                    Name = "ПК-53",
                    Year = 5,
                    DepartmentId = dr.GetIdByName("Кафедра маркетинга")
                };
                gr.Insert(group5);

                Group group6 = new Group()
                {
                    Name = "ПБ-325",
                    Year = 1,
                    DepartmentId = dr.GetIdByName("Кафедра бухгалтерского учета")
                };
                gr.Insert(group6);

                Group group7 = new Group()
                {
                    Name = "ПБ-221",
                    Year = 2,
                    DepartmentId = dr.GetIdByName("Кафедра бухгалтерского учета")
                };
                gr.Insert(group7);

                Group group8 = new Group()
                {
                    Name = "КК-52",
                    Year = 5,
                    DepartmentId = dr.GetIdByName("Кафедра кибербезопасности")
                };
                gr.Insert(group8);

                Group group9 = new Group()
                {
                    Name = "КК-55",
                    Year = 5,
                    DepartmentId = dr.GetIdByName("Кафедра искусственного интеллекта")
                };
                gr.Insert(group9);

                Group group10 = new Group()
                {
                    Name = "ДУ-105",
                    Year = 3,
                    DepartmentId = dr.GetIdByName("Кафедра дифф. уравнений")
                };
                gr.Insert(group10);
            }

            if (!db.GroupsCurators.Any())
            {
                GroupCuratorRepository gcr = new GroupCuratorRepository();
                GroupRepository gr = new GroupRepository();
                CuratorRepository cr = new CuratorRepository();

                GroupCurator gc1 = new GroupCurator() { GroupId = gr.GetIdByName("ПК-312"), CuratorId = cr.GetIdByName("Воробьев") };
                gcr.Insert(gc1);

                GroupCurator gc2 = new GroupCurator() { GroupId = gr.GetIdByName("ПК-211"), CuratorId = cr.GetIdByName("Иванова") };
                gcr.Insert(gc2);

                GroupCurator gc3 = new GroupCurator() { GroupId = gr.GetIdByName("ПК-100"), CuratorId = cr.GetIdByName("Собко") };
                gcr.Insert(gc3);

                GroupCurator gc4 = new GroupCurator() { GroupId = gr.GetIdByName("ПК-80"), CuratorId = cr.GetIdByName("Иванова") };
                gcr.Insert(gc4);

                GroupCurator gc5 = new GroupCurator() { GroupId = gr.GetIdByName("ПК-53"), CuratorId = cr.GetIdByName("Воробьев") };
                gcr.Insert(gc5);

                GroupCurator gc6 = new GroupCurator() { GroupId = gr.GetIdByName("ПБ-325"), CuratorId = cr.GetIdByName("Петрова") };
                gcr.Insert(gc6);

                GroupCurator gc7 = new GroupCurator() { GroupId = gr.GetIdByName("ПБ-221"), CuratorId = cr.GetIdByName("Петрова") };
                gcr.Insert(gc7);

                GroupCurator gc8 = new GroupCurator() { GroupId = gr.GetIdByName("КК-52"), CuratorId = cr.GetIdByName("Кабанов") };
                gcr.Insert(gc8);

                GroupCurator gc9 = new GroupCurator() { GroupId = gr.GetIdByName("КК-55"), CuratorId = cr.GetIdByName("Кабанов") };
                gcr.Insert(gc9);

                GroupCurator gc10 = new GroupCurator() { GroupId = gr.GetIdByName("ДУ-105"), CuratorId = cr.GetIdByName("Кабанов") };
                gcr.Insert(gc10);

            }

            if (!db.GroupsStudents.Any())
            {
                GroupStudentRepository gsr = new GroupStudentRepository();
                GroupRepository gr = new GroupRepository();
                StudentRepository sr = new StudentRepository();

                GroupStudent gs1 = new GroupStudent() { GroupId = gr.GetIdByName("ПК-312"), StudentId = sr.GetIdByName("Хачатрян") };
                gsr.Insert(gs1);

                GroupStudent gs2 = new GroupStudent() { GroupId = gr.GetIdByName("ПК-211"), StudentId = sr.GetIdByName("Шипков") };
                gsr.Insert(gs2);

                GroupStudent gs3 = new GroupStudent() { GroupId = gr.GetIdByName("ПК-100"), StudentId = sr.GetIdByName("Михайлов") };
                gsr.Insert(gs3);

                GroupStudent gs4 = new GroupStudent() { GroupId = gr.GetIdByName("ПК-80"), StudentId = sr.GetIdByName("Санкина") };
                gsr.Insert(gs4);

                GroupStudent gs5 = new GroupStudent() { GroupId = gr.GetIdByName("ПК-53"), StudentId = sr.GetIdByName("Корса") };
                gsr.Insert(gs5);

                GroupStudent gs6 = new GroupStudent() { GroupId = gr.GetIdByName("ПК-53"), StudentId = sr.GetIdByName("Блажко") };
                gsr.Insert(gs6);

                GroupStudent gs7 = new GroupStudent() { GroupId = gr.GetIdByName("ПБ-325"), StudentId = sr.GetIdByName("Глущенко") };
                gsr.Insert(gs7);

                GroupStudent gs8 = new GroupStudent() { GroupId = gr.GetIdByName("ПБ-221"), StudentId = sr.GetIdByName("Петров") };
                gsr.Insert(gs8);

                GroupStudent gs9 = new GroupStudent() { GroupId = gr.GetIdByName("КК-52"), StudentId = sr.GetIdByName("Иванов") };
                gsr.Insert(gs9);

                GroupStudent gs10 = new GroupStudent() { GroupId = gr.GetIdByName("КК-52"), StudentId = sr.GetIdByName("Кучернюк") };
                gsr.Insert(gs10);

                GroupStudent gs11 = new GroupStudent() { GroupId = gr.GetIdByName("КК-52"), StudentId = sr.GetIdByName("Кичуга") };
                gsr.Insert(gs11);
                
            }

            if (!db.Lectures.Any())
            {
                LectureRepository lr = new LectureRepository();
                SubjectRepository sr = new SubjectRepository();
                TeacherRepository tr = new TeacherRepository();
                
                Lecture l1 = new Lecture() 
                { 
                    LectureDate = DateTime.Parse("2025-09-23"),
                    SubjectId = sr.GetIdByName("Математический анализ"),
                    TeacherId = tr.GetIdByName("Бабич") 
                };
                lr.Insert(l1);

                Lecture l2 = new Lecture() 
                {
                    LectureDate = DateTime.Parse("2025-09-20"),
                    SubjectId = sr.GetIdByName("Макроекономика"),
                    TeacherId = tr.GetIdByName("Грач") 
                };
                lr.Insert(l2);

                Lecture l3 = new Lecture()
                {
                    LectureDate = DateTime.Parse("2025-09-20"),
                    SubjectId = sr.GetIdByName("Микроекономика"),
                    TeacherId = tr.GetIdByName("Грач")
                };
                lr.Insert(l3);

                Lecture l4 = new Lecture()
                {
                    LectureDate = DateTime.Parse("2025-09-21"),
                    SubjectId = sr.GetIdByName("Кибербезопасность"),
                    TeacherId = tr.GetIdByName("Шехов")
                };
                lr.Insert(l4);

                Lecture l5 = new Lecture()
                {
                    LectureDate = DateTime.Parse("2025-09-19"),
                    SubjectId = sr.GetIdByName("Теория маркетинга"),
                    TeacherId = tr.GetIdByName("Филатов")
                };
                lr.Insert(l5);
                
            }

            if (!db.GroupsLectures.Any())
            {
                GroupLectureRepository glr = new GroupLectureRepository();
                GroupRepository gr = new GroupRepository();
                LectureRepository lr = new LectureRepository();

                GroupLecture gl1 = new GroupLecture()
                {
                    GroupId = gr.GetIdByName("ПК-312"),
                    LectureId = lr.GetId("Теория маркетинга", "Филатов", DateTime.Parse("2025-09-19"))
                };
                glr.Insert(gl1);

                GroupLecture gl2 = new GroupLecture()
                {
                    GroupId = gr.GetIdByName("ПК-211"),
                    LectureId = lr.GetId("Теория маркетинга", "Филатов", DateTime.Parse("2025-09-19"))
                };
                glr.Insert(gl2);

                GroupLecture gl3 = new GroupLecture()
                {
                    GroupId = gr.GetIdByName("КК-52"),
                    LectureId = lr.GetId("Кибербезопасность", "Шехов", DateTime.Parse("2025-09-21"))
                };
                glr.Insert(gl3);

                GroupLecture gl4 = new GroupLecture()
                {
                    GroupId = gr.GetIdByName("ДУ-105"),
                    LectureId = lr.GetId("Математический анализ", "Бабич", DateTime.Parse("2025-09-23"))
                };
                glr.Insert(gl4);

                GroupLecture gl5 = new GroupLecture()
                {
                    GroupId = gr.GetIdByName("ПБ-221"),
                    LectureId = lr.GetId("Математический анализ", "Бабич", DateTime.Parse("2025-09-23"))
                };
                glr.Insert(gl5);
                                
            }          
        }
    }
}
