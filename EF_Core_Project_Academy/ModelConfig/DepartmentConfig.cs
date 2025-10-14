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
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> tb)
        {
            tb.ToTable("Departments");

            tb.HasKey(e => e.Id).HasName("PK_DepartmentId");
            tb.Property(e => e.Id).HasColumnName("departments_id");

            tb.Property(e => e.Building).HasColumnName("departments_building");
            tb.HasCheckConstraint("CC_DepartmentBuilding", "[departments_building] >= 1 AND [departments_building] <= 5");

            tb.Property(e => e.Financing).HasColumnName("departments_financing")
                .HasDefaultValueSql("('0')")
                .HasColumnType("money");
            

           tb.Property(e => e.Name).HasColumnName("departments_name")
                .HasColumnType("nvarchar(100)")
                .IsRequired();
                        
            tb.Property(e => e.FacultyId).HasColumnName("departments_facultyId");

            tb.HasIndex(e => new { e.Name, e.FacultyId }, "UQ_DepartmentNameFacultyId").IsUnique();

            tb.HasOne(d => d.Faculty).WithMany(p => p.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_departments_facultyId");

            
        }
    }
}
