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
            tb.HasKey(e => e.Id).HasName("PK_GroupLectureId");
            tb.Property(e => e.Id).HasColumnName("groupsLectures_id");
            
            tb.Property(e => e.GroupId).HasColumnName("groupsLectures_groupId");

            tb.Property(e => e.LectureId).HasColumnName("groupsLectures_lectureId");

            tb.HasOne(d => d.Group).WithMany(p => p.GroupLectures)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_groupsLectures_groupId");

            tb.HasOne(d => d.Lecture).WithMany(p => p.GroupLectures)
                .HasForeignKey(d => d.LectureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_groupsLectures_lectureId");
           
        }
    }
}
