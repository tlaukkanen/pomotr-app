using System;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PomotrApp.Data;

namespace PomotrApp.Tests
{
    public class DatabaseFixture : IDisposable
    {
		public ApplicationDbContext DbContext { get; private set; } =
			new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
				    .UseInMemoryDatabase(databaseName: "Pomotr")
				    .Options,
                Options.Create<OperationalStoreOptions>(
                    new OperationalStoreOptions()
                    {
                        ConfigureDbContext = opts => {
                            opts.UseInMemoryDatabase(databaseName: "Pomotr");
                        }
                    })
                );
        public DatabaseFixture() 
        {
            

        }

        public void Dispose()
        {
            DbContext = null;
        }
    }
    
}