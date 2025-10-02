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

            if (!db.Lectures.Any())
            {
                SubjectRepository sr = new SubjectRepository();
                TeacherRepository tr = new TeacherRepository();
                db.Lectures.AddRange(new Lecture[]
                {

                    new Lecture { LectureDate = DateOnly.Parse("2025-09-23"),
                                  SubjectId = sr.GetIdByName("Математический анализ"),
                                  TeacherId = tr.GetIdByName("Бабич") },

                    new Lecture { LectureDate = DateOnly.Parse("2025-09-20"),
                                  SubjectId = sr.GetIdByName("Макроекономика"),
                                  TeacherId = tr.GetIdByName("Грач") },

                    new Lecture { LectureDate = DateOnly.Parse("2025-09-20"),
                                  SubjectId = sr.GetIdByName("Микроекономика"),
                                  TeacherId = tr.GetIdByName("Грач") },

                    new Lecture { LectureDate = DateOnly.Parse("2025-09-21"),
                                  SubjectId = sr.GetIdByName("Кибербезопасность"),
                                  TeacherId = tr.GetIdByName("Шехов") },

                    new Lecture { LectureDate = DateOnly.Parse("2025-09-19"),
                                  SubjectId = sr.GetIdByName("Теория маркетинга"),
                                  TeacherId = tr.GetIdByName("Филатов") }
                });
                db.SaveChanges();
            }

            if (!db.Departments.Any())
            {
                FacultyRepository fr = new FacultyRepository();

                db.Departments.AddRange(new Department[]
                {
                    new Department { Building = 1,
                                    Financing = 100000,
                                    Name = "Кафедра алгебры",
                                    FacultyId = fr.GetIdByName ("Факультет математики") },

                    new Department { Building = 1,
                                    Financing = 110000,
                                    Name = "Кафедра мат. анализа",
                                    FacultyId = fr.GetIdByName ("Факультет математики") },

                    new Department { Building = 1,
                                    Financing = 80000,
                                    Name = "Кафедра дифф. уравнений",
                                    FacultyId = fr.GetIdByName ("Факультет математики") },

                    new Department { Building = 3,
                                    Financing = 200000,
                                    Name = "Кафедра программной инженерии",
                                    FacultyId = fr.GetIdByName ("Факультет компьютерных наук") },

                    new Department { Building = 3,
                                    Financing = 250000,
                                    Name = "Кафедра искусственного интеллекта",
                                    FacultyId = fr.GetIdByName ("Факультет компьютерных наук") },

                    new Department { Building = 3,
                                    Financing = 190000,
                                    Name = "Кафедра кибербезопасности",
                                    FacultyId = fr.GetIdByName ("Факультет компьютерных наук") },

                    new Department { Building = 2,
                                    Financing = 50000,
                                    Name = "Кафедра финансов",
                                    FacultyId = fr.GetIdByName ("Факультет економики") },

                    new Department { Building = 2,
                                    Financing = 60000,
                                    Name = "Кафедра бухгалтерского учета",
                                    FacultyId = fr.GetIdByName ("Факультет економики") },

                    new Department { Building = 2,
                                    Financing = 50000,
                                    Name = "Кафедра маркетинга",
                                    FacultyId = fr.GetIdByName ("Факультет економики") }
                });
                db.SaveChanges();
            }

            if (!db.Groups.Any())
            {
                DepartmentRepository dr = new DepartmentRepository();

                db.Groups.AddRange(new Group[]
                {
                    new Group { Name = "ПК-312",
                                Year = 1,
                                DepartmentId = dr.GetIdByName("Кафедра маркетинга") },

                    new Group { Name = "ПК-211",
                                Year = 2,
                                DepartmentId = dr.GetIdByName("Кафедра маркетинга") },

                    new Group { Name = "ПК-100",
                                Year = 3,
                                DepartmentId = dr.GetIdByName("Кафедра маркетинга") },

                    new Group { Name = "ПК-80",
                                Year = 4,
                                DepartmentId = dr.GetIdByName("Кафедра маркетинга")},

                    new Group { Name = "ПК-53",
                                Year = 5,
                                DepartmentId = dr.GetIdByName("Кафедра маркетинга") },

                    new Group { Name = "ПБ-325",
                                Year = 1,
                                DepartmentId = dr.GetIdByName("Кафедра бухгалтерского учета") },

                    new Group { Name = "ПБ-221",
                                Year = 2,
                                DepartmentId = dr.GetIdByName("Кафедра бухгалтерского учета")},

                    new Group { Name = "КК-52",
                                Year = 5,
                                DepartmentId = dr.GetIdByName("Кафедра кибербезопасности") },

                    new Group { Name = "КК-55",
                                Year = 5,
                                DepartmentId = dr.GetIdByName("Кафедра искусственного интеллекта") },

                    new Group { Name = "ДУ-105",
                                Year = 3,
                                DepartmentId = dr.GetIdByName("Кафедра дифф. уравнений") }

                });
                db.SaveChanges();
            }

            if (!db.GroupsCurators.Any())
            {
                GroupRepository gr = new GroupRepository();
                CuratorRepository cr = new CuratorRepository();

                db.GroupsCurators.AddRange(new GroupCurator[]
                {
                    new GroupCurator { GroupId = gr.GetIdByName("ПК-312"), CuratorId = cr.GetIdByName("Воробьев") },

                    new GroupCurator { GroupId = gr.GetIdByName("ПК-211"), CuratorId = cr.GetIdByName("Иванова") },

                    new GroupCurator { GroupId = gr.GetIdByName("ПК-100"), CuratorId = cr.GetIdByName("Собко") },

                    new GroupCurator { GroupId = gr.GetIdByName("ПК-80"), CuratorId = cr.GetIdByName("Иванова") },

                    new GroupCurator { GroupId = gr.GetIdByName("ПК-53"), CuratorId = cr.GetIdByName("Воробьев") },

                    new GroupCurator { GroupId = gr.GetIdByName("ПБ-325"), CuratorId = cr.GetIdByName("Петрова") },

                    new GroupCurator { GroupId = gr.GetIdByName("ПБ-221"), CuratorId = cr.GetIdByName("Петрова") },

                    new GroupCurator { GroupId = gr.GetIdByName("КК-52"), CuratorId = cr.GetIdByName("Кабанов") },

                    new GroupCurator { GroupId = gr.GetIdByName("КК-55"), CuratorId = cr.GetIdByName("Кабанов") },

                    new GroupCurator { GroupId = gr.GetIdByName("ДУ-105"), CuratorId = cr.GetIdByName("Кабанов")}
                });
                db.SaveChanges();
            }

            if (!db.GroupsLectures.Any())
            {
                GroupRepository gr = new GroupRepository();
                LectureRepository lr = new LectureRepository(db);

                db.GroupsLectures.AddRange(new GroupLecture[]
                {
                    new GroupLecture { GroupId = gr.GetIdByName("ПК-312"),
                                       LectureId = lr.GetId("Теория маркетинга", "Филатов", DateOnly.Parse("2025-09-19")) },

                    new GroupLecture { GroupId = gr.GetIdByName("ПК-211"),
                                       LectureId = lr.GetId("Теория маркетинга", "Филатов",DateOnly.Parse("2025-09-19")) },

                    new GroupLecture { GroupId = gr.GetIdByName("КК-52"),
                                       LectureId = lr.GetId("Кибербезопасность", "Шехов",DateOnly.Parse("2025-09-21")) },

                    new GroupLecture { GroupId = gr.GetIdByName("ДУ-105"),
                                       LectureId = lr.GetId("Математический анализ", "Бабич", DateOnly.Parse("2025-09-23")) },

                    new GroupLecture { GroupId = gr.GetIdByName("ПБ-221"),
                                       LectureId = lr.GetId("Математический анализ", "Бабич", DateOnly.Parse("2025-09-23")) }
                });
                db.SaveChanges();
            }

            if (!db.GroupsStudents.Any())
            {
                GroupRepository gr = new GroupRepository();
                StudentRepository sr = new StudentRepository();

                db.GroupsStudents.AddRange(new GroupStudent[]
                {
                    new GroupStudent { GroupId = gr.GetIdByName("ПК-312"), StudentId = sr.GetIdByName("Хачатрян") },

                    new GroupStudent { GroupId = gr.GetIdByName("ПК-211"), StudentId = sr.GetIdByName("Шипков") },

                    new GroupStudent { GroupId = gr.GetIdByName("ПК-100"), StudentId = sr.GetIdByName("Михайлов") },

                    new GroupStudent { GroupId = gr.GetIdByName("ПК-80"), StudentId = sr.GetIdByName("Санкина") },

                    new GroupStudent { GroupId = gr.GetIdByName("ПК-53"), StudentId = sr.GetIdByName("Корса") },

                    new GroupStudent { GroupId = gr.GetIdByName("ПК-53"), StudentId = sr.GetIdByName("Блажко") },

                    new GroupStudent { GroupId = gr.GetIdByName("ПБ-325"), StudentId = sr.GetIdByName("Глущенко") },

                    new GroupStudent { GroupId = gr.GetIdByName("ПБ-221"), StudentId = sr.GetIdByName("Петров") },

                    new GroupStudent { GroupId = gr.GetIdByName("КК-52"), StudentId = sr.GetIdByName("Иванов") },

                    new GroupStudent { GroupId = gr.GetIdByName("КК-52"), StudentId = sr.GetIdByName("Кучернюк") },

                    new GroupStudent { GroupId = gr.GetIdByName("КК-52"), StudentId = sr.GetIdByName("Кичуга") }
                });
                db.SaveChanges();
            }
        }
    }
}
