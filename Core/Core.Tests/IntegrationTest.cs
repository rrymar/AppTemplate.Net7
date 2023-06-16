using Core.Tests.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Tests
{

    [AutoRollback]
    public abstract class IntegrationTest<TDbContex, TStartup, TMigrationScripts>
        where TDbContex : DbContext
        where TStartup : class
    {
        private readonly TestApplicationFactory<TDbContex, TStartup, TMigrationScripts> factory;

        protected virtual bool CreateTransaction => false;

        protected readonly TDbContex DataContext;

        protected readonly IServiceProvider Services;

        private readonly IServiceScope scope;

        protected IntegrationTest(TestApplicationFactory<TDbContex, TStartup, TMigrationScripts> factory)
        {
            this.factory = factory;
            factory.Server.PreserveExecutionContext = true;

            scope = factory.Services.CreateScope();
            Services = scope.ServiceProvider;

            DataContext = scope.ServiceProvider.GetRequiredService<TDbContex>();

        }

        public virtual void Dispose()
        {
            scope?.Dispose();
        }
    }
}
