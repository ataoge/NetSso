using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ataoge.EntityFrameworkCore.Tests
{
    public class TestDbContext : EfIdentityDbContext<TestUser, TestRole>
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    public class TestUser: EfIdentityUser<int>
    {

    }

    public class TestRole : EfIdentityRole<int>
    {

    }
}