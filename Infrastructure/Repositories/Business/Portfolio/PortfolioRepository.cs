﻿using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Repositories.Portfolios;

namespace Infrastructure.Repositories.Business.Portfolio
{
    public class PortfolioRepository<TSpecificEntity> : Repository<TSpecificEntity>, IPortfolioRepository<TSpecificEntity> where TSpecificEntity : class, IEntityRoot, new()
    {
        public PortfolioRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}