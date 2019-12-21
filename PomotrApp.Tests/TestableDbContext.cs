using System;
using PomotrApp.Data;

namespace PomotrApp.Tests
{
    public class TestableDbContext : IDisposable
    {
        ApplicationDbContext context;

        public TestableDbContext() 
        {
            

        }

        public void Dispose()
        {
            context = null;
        }
    }
    
}