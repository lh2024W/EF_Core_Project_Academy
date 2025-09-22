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
    public class FacultyConfig : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> tb)
        {
            tb.ToTable("Faculties");

            tb.HasKey(e => e.Id).HasName("PK_FacultyId");
            tb.Property(e => e.Id).HasColumnName("faculties_id");

            tb.HasIndex(e => e.Name, "UQ_FacultyName").IsUnique();
            tb.Property(e => e.Name).HasColumnName("faculties_name")
                .HasColumnType("nvarchar(100)")
                .IsRequired();

        }
        
    }
    
}
