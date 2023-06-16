using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Tests.Database
{
    public static class TestDatabasesExtensions
    {
        private static readonly object locker = new object();

        private static bool dbInitialized;

        public static void InitTestDatabases<TContext>(this TContext dbContext,
            Assembly migrationAsssembly,
            params List<ITestMigration<TContext>>[] testMigrations)
            where TContext : DbContext
        {
            lock (locker)
            {
                if (dbInitialized) return;

                var db = new TestDatabaseMigrator(dbContext);
                db.Create();

                db.RunMigrations(migrationAsssembly);
                foreach (var testMigration in testMigrations)
                {
                    db.RunMigrations(testMigration);
                }


                dbInitialized = true;
            }
        }
    }
}
