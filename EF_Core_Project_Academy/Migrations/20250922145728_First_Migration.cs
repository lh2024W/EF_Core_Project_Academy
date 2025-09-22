using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core_Project_Academy.Migrations
{
    /// <inheritdoc />
    public partial class First_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curators",
                columns: table => new
                {
                    curators_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    curators_name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    curators_surname = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuratorId", x => x.curators_id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    faculties_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    faculties_name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyId", x => x.faculties_id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    students_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    students_name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    students_rating = table.Column<int>(type: "int", nullable: false),
                    students_surname = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentId", x => x.students_id);
                    table.CheckConstraint("CC_StudentRating", "[students_rating] > 0 AND [students_rating] <= 5");
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    subjects_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subjects_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectId", x => x.subjects_id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    teachers_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teachers_IsProfessor = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    teachers_name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    teachers_salary = table.Column<decimal>(type: "money", nullable: false),
                    teachers_surname = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherId", x => x.teachers_id);
                    table.CheckConstraint("CC_TeacherSalary", "[teachers_salary] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    departments_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departments_building = table.Column<int>(type: "int", nullable: false),
                    departments_financing = table.Column<decimal>(type: "money", nullable: false, defaultValueSql: "('0')"),
                    departments_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    departments_facultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentId", x => x.departments_id);
                    table.CheckConstraint("CC_DepartmentBuilding", "[departments_building] >= 1 AND [departments_building] <= 5");
                    table.ForeignKey(
                        name: "FK_departments_facultyId",
                        column: x => x.departments_facultyId,
                        principalTable: "Faculties",
                        principalColumn: "faculties_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    lectures_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lectures_date = table.Column<DateOnly>(type: "date", nullable: false),
                    lectures_subjectId = table.Column<int>(type: "int", nullable: false),
                    lectures_teacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureId", x => x.lectures_id);
                    table.CheckConstraint("CC_LectureDate", "[lectures_date] <= GETDATE()");
                    table.ForeignKey(
                        name: "FK_lectures_subjectId",
                        column: x => x.lectures_subjectId,
                        principalTable: "Subjects",
                        principalColumn: "subjects_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lectures_teacherId",
                        column: x => x.lectures_teacherId,
                        principalTable: "Teachers",
                        principalColumn: "teachers_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    groups_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    groups_name = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    groups_year = table.Column<int>(type: "int", nullable: false),
                    groups_departmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupId", x => x.groups_id);
                    table.CheckConstraint("CC_GroupYear", "[groups_year] >= 1 AND [groups_year] <= 5");
                    table.ForeignKey(
                        name: "FK_groups_departmentId",
                        column: x => x.groups_departmentId,
                        principalTable: "Departments",
                        principalColumn: "departments_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsCurators",
                columns: table => new
                {
                    groupsCurators_curatorId = table.Column<int>(type: "int", nullable: false),
                    groupsCurators_groupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsCurators", x => new { x.groupsCurators_curatorId, x.groupsCurators_groupId });
                    table.ForeignKey(
                        name: "FK_groupsCurators_curatorId",
                        column: x => x.groupsCurators_curatorId,
                        principalTable: "Curators",
                        principalColumn: "curators_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupsCurators_groupId",
                        column: x => x.groupsCurators_groupId,
                        principalTable: "Groups",
                        principalColumn: "groups_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsLectures",
                columns: table => new
                {
                    groupsLectures_groupId = table.Column<int>(type: "int", nullable: false),
                    groupsLectures_lectureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsLectures", x => new { x.groupsLectures_groupId, x.groupsLectures_lectureId });
                    table.ForeignKey(
                        name: "FK_groupsLectures_groupId",
                        column: x => x.groupsLectures_groupId,
                        principalTable: "Groups",
                        principalColumn: "groups_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupsLectures_lectureId",
                        column: x => x.groupsLectures_lectureId,
                        principalTable: "Lectures",
                        principalColumn: "lectures_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsStudents",
                columns: table => new
                {
                    groupsStudents_groupId = table.Column<int>(type: "int", nullable: false),
                    groupsStudents_studentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsStudents", x => new { x.groupsStudents_groupId, x.groupsStudents_studentId });
                    table.ForeignKey(
                        name: "FK_groupsStudents_groupId",
                        column: x => x.groupsStudents_groupId,
                        principalTable: "Groups",
                        principalColumn: "groups_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupsStudents_studentId",
                        column: x => x.groupsStudents_studentId,
                        principalTable: "Students",
                        principalColumn: "students_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_departments_facultyId",
                table: "Departments",
                column: "departments_facultyId");

            migrationBuilder.CreateIndex(
                name: "UQ_DepartmentName",
                table: "Departments",
                column: "departments_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_FacultyName",
                table: "Faculties",
                column: "faculties_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_groups_departmentId",
                table: "Groups",
                column: "groups_departmentId");

            migrationBuilder.CreateIndex(
                name: "UQ_GroupsName",
                table: "Groups",
                column: "groups_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupsCurators_groupsCurators_groupId",
                table: "GroupsCurators",
                column: "groupsCurators_groupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsLectures_groupsLectures_lectureId",
                table: "GroupsLectures",
                column: "groupsLectures_lectureId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsStudents_groupsStudents_studentId",
                table: "GroupsStudents",
                column: "groupsStudents_studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_lectures_subjectId",
                table: "Lectures",
                column: "lectures_subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_lectures_teacherId",
                table: "Lectures",
                column: "lectures_teacherId");

            migrationBuilder.CreateIndex(
                name: "UQ_SubjectName",
                table: "Subjects",
                column: "subjects_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsCurators");

            migrationBuilder.DropTable(
                name: "GroupsLectures");

            migrationBuilder.DropTable(
                name: "GroupsStudents");

            migrationBuilder.DropTable(
                name: "Curators");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
