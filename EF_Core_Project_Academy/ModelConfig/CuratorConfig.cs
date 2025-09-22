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
    public class CuratorConfig : IEntityTypeConfiguration<Curator>
    {
        public void Configure(EntityTypeBuilder<Curator> tb)
        {
            tb.ToTable("Curators");

            tb.HasKey(e => e.Id).HasName("PK_CuratorId");
            tb.Property(e => e.Id).HasColumnName("curators_id");
             
            tb.Property(e => e.Name).HasColumnName("curators_name")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            tb.Property(e => e.Surname).HasColumnName("curators_surname")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

        }

    }
}
