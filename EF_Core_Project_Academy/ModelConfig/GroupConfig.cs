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
    public class GroupConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> tb)
        {
            tb.ToTable("Groups");

            tb.HasKey(e => e.Id).HasName("PK_GroupId");
            tb.Property(e => e.Id).HasColumnName("groups_id");

            tb.HasIndex(e => e.Name, "UQ__Groups__86DEB79295B494D0").IsUnique();
            tb.Property(e => e.Name).HasColumnName("groups_name")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            tb.Property(e => e.Year).HasColumnName("groups_year");
            tb.HasCheckConstraint("CC_GroupYear", "[groups_year] >= 1 AND [groups_year] <= 5");

            tb.Property(e => e.DepartmentId).HasColumnName("groups_departmentId");
            
            
            tb.HasOne(d => d.Department).WithMany(p => p.Groups)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_groups_departmentId");
            
        }
    }
}
