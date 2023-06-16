using Microsoft.EntityFrameworkCore;

namespace Core.Tests.Database
{
    public interface ITestMigration<T>
         where T : DbContext
    {
        string Name { get; }

        void Execute(T context);
    }
}
