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
            tb.HasKey(e => e.Id).HasName("PK_GroupCuratorId");
            tb.Property(e => e.Id).HasColumnName("groupsCurators_id");

            tb.Property(e => e.CuratorId).HasColumnName("groupsCurators_curatorId");

            tb.Property(e => e.GroupId).HasColumnName("groupsCurators_groupId");

            tb.HasOne(d => d.Curator).WithMany(p => p.GroupCurators)
                .HasForeignKey(d => d.CuratorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_groupsCurators_curatorId");

            tb.HasOne(d => d.Group).WithMany(p => p.GroupCurators)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_groupsCurators_groupId");
            
        }
    }
}
