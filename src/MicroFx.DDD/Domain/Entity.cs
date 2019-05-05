using System;
using System.Collections.Generic;
using System.Text;

namespace MicroFx.DDD.Domain
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
