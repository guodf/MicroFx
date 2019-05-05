using System;
using System.Collections.Generic;
using System.Text;
using MicroFx.DDD.Domain;
using MicroFx.DDD.Repository;
using MicroFx.DDD.UnitOfWork;

namespace MicroFx.DDD
{
    public interface IUnitOfWorkRepository:IUnitOfWork
    {
        T Repository<T>();
    }
}
