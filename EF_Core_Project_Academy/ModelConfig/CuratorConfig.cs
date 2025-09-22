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
            tb.HasKey(e => e.Id).HasName("PK_СuratorId");
            tb.Property(e => e.Id).HasColumnName("сurators_id");
             
            tb.Property(e => e.Name).HasColumnName("curators_name")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            tb.Property(e => e.Surname).HasColumnName("curators_surname")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

        }

    }
}
