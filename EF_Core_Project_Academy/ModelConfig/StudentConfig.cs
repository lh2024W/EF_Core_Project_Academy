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
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> tb)
        {
            tb.HasKey(e => e.Id).HasName("PK_StudentId");
            tb.Property(e => e.Id).HasColumnName("students_id");

            tb.Property(e => e.Name).HasColumnName("students_name")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            tb.Property(e => e.Rating).HasColumnName("students_rating");
            tb.HasCheckConstraint("CC_StudentRating", "[students_rating] > 0 AND [students_rating] <= 5");

            tb.Property(e => e.Surname).HasColumnName("students_surname")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

        }
    }
}
