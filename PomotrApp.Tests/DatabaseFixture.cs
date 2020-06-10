using System;
using System.Linq;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PomotrApp.Data;
using PomotrApp.Models;
using Snapper;
using Snapper.Attributes;
using Xunit;

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
            GenerateTestData();
        }

        private void GenerateTestData()
        {
            var family = new Family
            {
                Id = Guid.Parse("4e7ac2c6-bfd1-44a8-9b0b-aa8db0cb3d00"),
                Name = "Testers"
            };
            DbContext.Families.Add(family);

            var jackEmailAddress = "jack@testers.com";
            var jack = new FamilyMember
            {
                Id = Guid.Parse("b408742a-3309-4b01-bff7-b031409f44ef").ToString(),
                Email = jackEmailAddress,
                UserName = jackEmailAddress,
                NormalizedUserName = jackEmailAddress.ToUpper()
            };
            DbContext.FamilyMembers.Add(jack);
            DbContext.SaveChanges();
        }

        [Fact]
        [UpdateSnapshots]
        public async void GetFamilyTest()
        {
            var family = await DbContext.Families
                .AsNoTracking()
                .Where(f => f.Name == "Testers")
                .FirstOrDefaultAsync();
        
            family.ShouldMatchSnapshot();
        }

        public void Dispose()
        {
            DbContext = null;
        }
    }
    
}