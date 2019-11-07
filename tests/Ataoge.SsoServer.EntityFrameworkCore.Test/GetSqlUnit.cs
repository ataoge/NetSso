using System;
using System.Linq;
using Ataoge.EntityFrameworkCore;
using Ataoge.EntityFrameworkCore.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ataoge.SsoServer.EntityFrameworkCore.Test
{
    public class SqlUnit
    {
        [Fact]
        public void GetSql()
        {
            var services = new ServiceCollection();
            services.AddDbContext<TestDbContext>(b => {
                b.UseSqlite("DataSource=../../src/Ataoge.SSoServer.Web/IdSvr.db");
            });
            var sp = services.BuildServiceProvider();

            var testDbContext = sp.GetService<TestDbContext>();
            var userQuery = testDbContext.Users;
            var aa = "aa";
            var bb = userQuery.Where(u => u.UserName == aa ).ToSql();

        }
    }
}
