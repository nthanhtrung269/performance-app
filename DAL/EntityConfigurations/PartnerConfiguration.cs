﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Core.Domain.Partner;

namespace DAL.EntityConfigurations
{
    public class PartnerConfiguration : EntityTypeConfiguration<BasePartner>
    {
        public PartnerConfiguration()
        {
            HasKey(p => p.Id);

            ToTable("tbl_Partner");

            Property(o => o.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("PartnerId");

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("PartnerName");

            Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("PartnerNumber");

            HasRequired(p => p.Organization)
                .WithRequiredPrincipal(o => o.Partner);

            HasMany(p => p.Contacts)
                .WithRequired(c => c.Partner)
                .HasForeignKey(c => c.PartnerId)
                .WillCascadeOnDelete(false);

            Map<AssetManager>(p => p.Requires("PartnerType").HasValue(1));
        }
    }
}
