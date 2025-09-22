using EF_Core_Project_Academy.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Project_Academy.ModelConfig
{
    public class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> tb)
        {
            tb.ToTable("Teachers");
            
            tb.HasKey(e => e.Id).HasName("PK_TeacherId");
            tb.Property(e => e.Id).HasColumnName("teachers_id");
            
            tb.Property(e => e.IsProfessor).HasColumnName("teachers_IsProfessor")
                .HasDefaultValue(false)
                .HasColumnType("bit");

            tb.Property(e => e.Name).HasColumnName("teachers_name")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            

            tb.Property(e => e.Salary)
                .HasColumnType("money")
                .HasColumnName("teachers_salary");
            tb.HasCheckConstraint("CC_TeacherSalary", "[teachers_salary] >= 0");

            tb.Property(e => e.Surname).HasColumnName("teachers_surname")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

        }
    }
}
