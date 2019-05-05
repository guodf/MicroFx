using Micro.EntityFrameworkCore.RemoveForeignKey.MySql;

using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseRemoveForeignKeyService(this DbContextOptionsBuilder options)
        {
            options.ReplaceService<IMigrationsSqlGenerator, CustomMigrationsSqlGeneratore>();
            return options;
        }
    }
}
