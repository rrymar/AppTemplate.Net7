using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tests.Database
{
    public class TestMigrationExecutor
    {
        private const string MigrationHistorySql = @"
            INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
            VALUES (N'{0}', N'3.1.0');
        ";

        public void Execute<T>(DbContext context, IEnumerable<ITestMigration<T>> testMigrations)
           where T : DbContext
        {
            foreach (var migration in testMigrations.OrderBy(m => m.Name))
            {

                var t = context.Database.BeginTransaction();
                try
                {
                    migration.Execute((T)context);

                    var addHistory = string.Format(MigrationHistorySql, migration.Name);
                    context.Database.ExecuteSqlRaw(addHistory);

                    t.Commit();
                }
                catch (Exception ex)
                {
                    t.Rollback();
                    throw new Exception(migration.GetType().Name + Environment.NewLine + ex.Message, ex);
                }
                finally
                {
                    t.Dispose();
                }
            }
        }
    }
}
