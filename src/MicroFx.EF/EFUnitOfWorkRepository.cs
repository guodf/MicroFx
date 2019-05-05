using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MicroFx.DDD;
using Microsoft.EntityFrameworkCore;

namespace MicroFx.EFCore
{
    public abstract class EFUnitOfWorkRepository : IUnitOfWorkRepository
    {
        protected DbContext _dbContext;
        protected EFUnitOfWorkRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public abstract EFRepository Repository<EFRepository>();

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
