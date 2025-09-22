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
    public class GroupLectureConfig : IEntityTypeConfiguration<GroupLecture>
    {
        public void Configure(EntityTypeBuilder<GroupLecture> tb)
        {
            tb.ToTable("GroupsLectures");
            
            tb.HasKey(t => new { t.GroupId, t.LectureId });

            tb.Property(e => e.GroupId).HasColumnName("groupsLectures_groupId").IsRequired();

            tb.Property(e => e.LectureId).HasColumnName("groupsLectures_lectureId").IsRequired();

            tb.HasOne(d => d.Group).WithMany(p => p.GroupsLectures)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_groupsLectures_groupId");

            tb.HasOne(d => d.Lecture).WithMany(p => p.GroupsLectures)
                .HasForeignKey(d => d.LectureId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_groupsLectures_lectureId");
           
        }
    }
}
