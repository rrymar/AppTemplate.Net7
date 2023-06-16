using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Tests.Database
{
    public class TestDatabaseMigrator
    {
        private readonly DbContext context;

        public TestDatabaseMigrator(DbContext context)
        {
            this.context = context;
        }

        public void Create()
        {
            //Create DB
            context.Database.Migrate();
        }

        public void RunMigrations(Assembly resourceAssembly)
        {
            var toRun = GetPendingMigrations(resourceAssembly);

            var executor = new SqlMigrationExecutor();
            executor.Execute(context, resourceAssembly, toRun);
        }

        public IOrderedEnumerable<string> GetPendingMigrations(Assembly resourceAssembly)
        {
            var appliedMigrations = GetAppliedMigrations();
            var assemblyMigrations = resourceAssembly.GetManifestResourceNames();
            return assemblyMigrations.Where(r => r.EndsWith(".sql") && !appliedMigrations.Contains(GetMigrationId(r)))
                .OrderBy(r => r);
        }

        public void RunMigrations<T>(IEnumerable<ITestMigration<T>> testMigrations)
            where T : DbContext
        {
            var appliedMigrations = GetAppliedMigrations();
            var toRun = testMigrations.Where(m => !appliedMigrations.Contains(m.Name)).ToList();

            var executor = new TestMigrationExecutor();
            executor.Execute(context, toRun);
        }

        private static string GetMigrationId(string resourceName)
        {
            return resourceName.Replace(".sql", string.Empty).Split('.').Last();
        }

        private HashSet<string> GetAppliedMigrations()
        {
            var migrations = new HashSet<string>();
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "IF OBJECT_ID(N'__EFMigrationsHistory') IS NOT NULL SELECT MigrationId FROM __EFMigrationsHistory";
                if(command.Connection.State != System.Data.ConnectionState.Open)
                    command.Connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        migrations.Add(reader.GetString(0));
                    }
                }
            }
            return migrations;
        }
    }
}
