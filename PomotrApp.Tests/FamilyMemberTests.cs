using System;
using PomotrApp.Models;
using Snapper;
using Snapper.Attributes;
using Xunit;

namespace PomotrApp.Tests
{

    public class FamilyMemberTests
    {
        [Fact]
        //[UpdateSnapshots]
        public void FamilyMemberTest()
        {
            string username = "John";

            var family = new Family
            {
                Id = Guid.Parse("f063554f-7499-4fcd-9942-304cd6047edf"),
                Name = "Testers Family"
            };

            var familyMember = new FamilyMember
            {
                Id = Guid.Parse("96d9c275-4f80-4ec6-ae18-6d044cf888a2").ToString(),
                NormalizedUserName = username.ToUpper(),
                UserName = username,
                Family = family
            };

            familyMember.UserName.ShouldMatchSnapshot();
        }
        
    }

}