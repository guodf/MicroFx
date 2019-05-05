using System;
using System.Collections.Generic;
using System.Text;

namespace MicroFx.DDD.Domain
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
