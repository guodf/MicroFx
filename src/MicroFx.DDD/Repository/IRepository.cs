using System;
using System.Collections.Generic;
using System.Text;
using MicroFx.DDD.Domain;

namespace MicroFx.DDD.Repository
{
    public interface IRepository<TEntity> : IRepository<TEntity,int>   
        where TEntity:class,IEntity<int>
    {
    }
    public interface IRepository<TEntity,TPrimaryKey> where TEntity:class,IEntity<TPrimaryKey>
    {
    }
}
