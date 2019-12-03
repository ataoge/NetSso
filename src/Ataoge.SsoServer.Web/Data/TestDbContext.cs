using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ataoge.SsoServer.Web.Data
{
    public class TestDbContext : IdentityDbContext<IdentityUser>
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

   

        
    }
}