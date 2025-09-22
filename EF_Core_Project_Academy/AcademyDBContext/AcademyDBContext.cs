using EF_Core_Project_Academy.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.AcademyDBContext
{
    public partial class AcademyDBContext : DbContext
    {
        public AcademyDBContext()
        {

        }
        public AcademyDBContext(DbContextOptions<AcademyDBContext> options) : base(options)
        {

        }

        public virtual DbSet<Model.Group> Groups { get; set; }

        public virtual DbSet<Curator> Curators { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Faculty> Faculties { get; set; }

        public virtual DbSet<GroupCurator> GroupsCurators { get; set; }

        public virtual DbSet<GroupLecture> GroupsLectures { get; set; }

        public virtual DbSet<GroupStudent> GroupsStudents { get; set; }

        public virtual DbSet<Lecture> Lectures { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=WIN-UKQRC56FDU3;Database=ProjectAcademyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
