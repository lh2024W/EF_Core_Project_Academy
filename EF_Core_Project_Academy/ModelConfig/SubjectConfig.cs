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
    public class SubjectConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> tb)
        {
            tb.ToTable("Subjects");

            tb.HasKey(e => e.Id).HasName("PK_SubjectId");
            tb.Property(e => e.Id).HasColumnName("subjects_id");

            tb.HasIndex(e => e.Name, "UQ_SubjectName").IsUnique();
            tb.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("subjects_name");

        }
    }
}
