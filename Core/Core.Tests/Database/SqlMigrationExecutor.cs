using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Tests.Database
{
    public class SqlMigrationExecutor
    {
        public void Execute(DbContext context, Assembly assembly, IEnumerable<string> sqls)
        {
            foreach (var sqlName in sqls)
            {
                var t = context.Database.BeginTransaction();
                try
                {
                    var sql = SqlFromResource(assembly, sqlName, false);
                    var regex = new Regex(@"^\s*GO\s*$", RegexOptions.Multiline);
                    var sqlParts = regex.Split(sql);

                    if (sql.Contains("MEMORY_OPTIMIZED = ON"))
                    {
                        t.Commit();
                        t.Dispose();
                        ExecuteSql(context, sqlName, null, sqlParts);
                        t = context.Database.BeginTransaction();
                    }
                    else
                    {
                        ExecuteSql(context, sqlName, t, sqlParts);
                    }
                    t.Commit();

                }
                finally
                {
                    t.Dispose();
                }

            }
        }

        private static void ExecuteSql(DbContext context, string sqlName, IDbContextTransaction t, string[] sqlParts)
        {
            foreach (var part in sqlParts)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(part))
                        continue;

                    context.Database.ExecuteSqlRaw(part);
                }
                catch (Exception ex)
                {
                    t?.Rollback();
                    throw new Exception(sqlName + Environment.NewLine + ex.Message, ex);
                }
            }
        }


        private string SqlFromResource(Assembly assembly, string resourceName, bool addAssemblyName = true)
        {
            var streamPath = addAssemblyName ? $"{assembly.GetName().Name}.{resourceName.Replace("/", ".")}" : resourceName;
            var resourceStream = assembly.GetManifestResourceStream(streamPath);

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
