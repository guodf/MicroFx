using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using MicroFx.EFCore.RemoveForeignKey;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;

namespace Micro.EntityFrameworkCore.RemoveForeignKey.MySql
{
    public class CustomMigrationsSqlGeneratore : MySqlMigrationsSqlGenerator
    {
        public CustomMigrationsSqlGeneratore(MigrationsSqlGeneratorDependencies dependencies, IMigrationsAnnotationProvider migrationsAnnotations, IMySqlOptions options) : base(dependencies, migrationsAnnotations, options)
        {
        }

        protected override void Generate(CreateTableOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            RemoveForeignKeysHelper.ExecuForeignKeys(operation);
            base.Generate(operation, model, builder);
        }
    }
}
