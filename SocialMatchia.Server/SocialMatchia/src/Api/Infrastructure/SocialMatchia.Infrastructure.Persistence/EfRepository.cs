﻿using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Infrastructure.Persistence.Context;

namespace SocialMatchia.Infrastructure.Persistence
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
    {
        private readonly SocialMatchiaDbContext _dbContext;

        public EfRepository(SocialMatchiaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetUpdateStateChangedProperties(T entity, string[] changedProperties)
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;

            foreach (var property in _dbContext.Entry(entity).Properties)
            {
                property.IsModified = false;
            }

            foreach (var changedProperty in changedProperties)
            {
                _dbContext.Entry(entity).Property(changedProperty).IsModified = true;
            }
        }
    }
}
