﻿using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Portfolios;

namespace Infrastructure.EntityConfigurations.BusinessConfigurations.PortfolioConfigurations
{
    public class PortfolioConfiguration : BaseEntityConfiguration<Portfolio>
    {
        public PortfolioConfiguration()
        {
            ToTable("Portfolio");

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("PortfolioId");

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("PortfolioName");

            HasRequired(p => p.Account)
                .WithMany(a => a.Portfolios)
                .WillCascadeOnDelete(false);

            HasMany(c => c.Positions)
                .WithRequired(p => p.Portfolio)
                .HasForeignKey(p => p.PortfolioId)
                .WillCascadeOnDelete(false);
        }
    }
}