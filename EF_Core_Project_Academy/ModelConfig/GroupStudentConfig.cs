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
    public class GroupStudentConfig : IEntityTypeConfiguration<GroupStudent>
    {
        public void Configure(EntityTypeBuilder<GroupStudent> tb)
        {
            tb.HasKey(e => e.Id).HasName("PK_GroupStudentId");
            tb.Property(e => e.Id).HasColumnName("groupsStudents_id");

            tb.Property(e => e.GroupId).HasColumnName("groupsStudents_groupId");

            tb.Property(e => e.StudentId).HasColumnName("groupsStudents_studentId");

            tb.HasOne(d => d.Group).WithMany(p => p.GroupStudents)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_groupsStudents_groupId");

            tb.HasOne(d => d.Student).WithMany(p => p.GroupStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_groupsStudents_studentId");
            
        }
    }
}
