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
            tb.ToTable("GroupsStudents");
            
            tb.HasKey(t => new { t.GroupId, t.StudentId });

            tb.Property(e => e.GroupId).HasColumnName("groupsStudents_groupId").IsRequired();

            tb.Property(e => e.StudentId).HasColumnName("groupsStudents_studentId").IsRequired();

            tb.HasOne(d => d.Group).WithMany(p => p.GroupsStudents)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_groupsStudents_groupId");

            tb.HasOne(d => d.Student).WithMany(p => p.GroupsStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_groupsStudents_studentId");
            
        }
    }
}
