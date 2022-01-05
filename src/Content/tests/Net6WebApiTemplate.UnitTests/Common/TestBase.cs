using Net6WebApiTemplate.Persistence;
using System;

namespace Net6WebApiTemplate.UnitTests.Common
{
    public class TestBase : IDisposable
    {
        protected readonly Net6WebApiTemplateDbContext _context;
        public TestBase()
        {
            _context = Net6WebApiTemplateDbContextFactory.Create();
        }
        public void Dispose()
        {
            Net6WebApiTemplateDbContextFactory.Destroy(_context);
        }
    }
}