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
    public class GroupCuratorConfig : IEntityTypeConfiguration<GroupCurator>
    {
        public void Configure(EntityTypeBuilder<GroupCurator> tb)
        {
            tb.ToTable("GroupsCurators");

            tb.HasKey(e => e.Id).HasName("PK_GroupsCuratorsId");
            tb.Property(e => e.Id).HasColumnName("groupsCurators_id");

            tb.HasIndex(e => new { e.CuratorId, e.GroupId }).IsUnique();

            tb.Property(e => e.CuratorId).HasColumnName("groupsCurators_curatorId").IsRequired();

            tb.Property(e => e.GroupId).HasColumnName("groupsCurators_groupId").IsRequired();

            tb.HasOne(d => d.Curator).WithMany(p => p.GroupsCurators)
                .HasForeignKey(d => d.CuratorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_groupsCurators_curatorId");

            tb.HasOne(d => d.Group).WithMany(p => p.GroupsCurators)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_groupsCurators_groupId");
            
        }
    }
}
