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
    public class LectureConfig : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> tb)
        {
            tb.HasKey(e => e.Id).HasName("PK_LectureId");
            tb.Property(e => e.Id).HasColumnName("lectures_id");

            tb.Property(e => e.LectureDate).HasColumnName("lectures_date");
            tb.HasCheckConstraint("CC_LectureDate", "[lectures_date] <= GETDATE()");


            tb.Property(e => e.SubjectId).HasColumnName("lectures_subjectId");

            tb.Property(e => e.TeacherId).HasColumnName("lectures_teacherId");

            tb.HasOne(d => d.Subject).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_lectures_subjectId");

            tb.HasOne(d => d.Teacher).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_lectures_teacherId");
            
        }
    }
}
