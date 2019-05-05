using MicroFx.EFCore.RemoveForeignKey;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Micro.EntityFrameworkCore.RemoveForeignKey.MySql
{
    public class CustomMigrationsSqlGeneratore : SqlServerMigrationsSqlGenerator
    {
        public CustomMigrationsSqlGeneratore( MigrationsSqlGeneratorDependencies dependencies,  IMigrationsAnnotationProvider migrationsAnnotations) : base(dependencies, migrationsAnnotations)
        {
        }

        protected override void Generate(CreateTableOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            RemoveForeignKeysHelper.ExecuForeignKeys(operation);
            base.Generate(operation, model, builder);
        }
    }
}
