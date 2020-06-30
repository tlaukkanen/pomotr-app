using System;
using PomotrApp.Models;
using Snapper.Attributes;
using Snapper;
using Xunit;

namespace PomotrApp.Tests
{
    public class FamilyTests
    {
        [Fact]
        //[UpdateSnapshots]
        public void FamilyTest()
        {
            Family family = new Family 
            {
                Id = Guid.Parse("930c493d-8876-476d-bcfc-4f4d18f2ec18"),
                Name = "Test Family"                
            };

            family.ShouldMatchSnapshot();
        }
    }
}
