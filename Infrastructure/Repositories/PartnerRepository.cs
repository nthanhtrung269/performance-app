﻿using Core.Interfaces;
using Core.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class PartnerRepository<TSpecificEntity> : Repository<TSpecificEntity>, IPartnerRepository<TSpecificEntity> where TSpecificEntity : class, IEntityRoot, new()
    {
        public PartnerRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
