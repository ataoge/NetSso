using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ataoge.SsoServer.Web.Data
{
    public class ApplicationDbContext : EfIdentityDbContext<ApplicationUser, ApplicationRole, int>//, IConfigurationDbContext, IPersistedGrantDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
