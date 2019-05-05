using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroFx.DDD.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();
    }
}
