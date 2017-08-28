﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Core.Domain.Partners;
using Core.Enums.Domain;

namespace Infrastructure.EntityConfigurations.PartnerConfigurations
{
    public class PartnerConfiguration : EntityTypeConfiguration<Partner>
    {
        public PartnerConfiguration()
        {
            Property(c => c.IsDeleted).HasColumnName("IsDeleted");

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

            HasMany(p => p.Organizations)
                .WithMany(o => o.Partners)
                .Map(m => m.ToTable("tbl_InstitutionPartners").MapLeftKey("PartnerId").MapRightKey("InstitutionId"));

            HasMany(p => p.Contacts)
                .WithRequired(c => c.Partner)
                .HasForeignKey(c => c.PartnerId)
                .WillCascadeOnDelete(false);

            HasMany(p => p.Accounts)
                .WithMany(a => a.Partners)
                .Map(m => m.ToTable("tbl_PartnerAccounts").MapLeftKey("PartnerId").MapRightKey("AccountId"));

            Map<AssetManager>(p => p.Requires("PartnerType").HasValue((int)PartnerType.AssetManager));
        }
    }
}
