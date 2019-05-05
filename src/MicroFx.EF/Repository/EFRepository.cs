using System;
using System.Collections.Generic;
using System.Text;
using MicroFx.DDD.Domain;
using MicroFx.DDD.Repository;
using Microsoft.EntityFrameworkCore;

namespace MicroFx.EFCore.Repository
{
    public interface IEFRepository<TEntity, TPrimaryKey> : IRepository<TEntity,TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        DbSet<TEntity> Entities { get; }
    }

    public abstract class EFRepository<TEntity, TPrimaryKey> : IEFRepository<TEntity,TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public DbSet<TEntity> Entities { get; set; }

        public EFRepository(DbSet<TEntity> entities)
        {
            Entities = entities;
        }
    }
}
