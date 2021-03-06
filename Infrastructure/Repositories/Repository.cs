﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        protected readonly ApplicationDbContext Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        protected IQueryable<TEntity> GetAll()
        {
            return _entities.AsQueryable();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public void Add(TEntity entity)
        {
            _entities.AddOrUpdate(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }
        
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
