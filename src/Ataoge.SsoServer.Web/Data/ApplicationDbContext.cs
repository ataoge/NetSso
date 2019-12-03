using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ataoge.SsoServer.Web.Data
{
    public class ApplicationDbContext : EfIdentityDbContext<ApplicationUser, ApplicationRole, int>, IConfigurationDbContext, IPersistedGrantDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly); // Here UseConfiguration is any IEntityTypeConfiguration
            base.OnModelCreating(builder);
        }

         /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public DbSet<Client> Clients { get; set; }
        /// <summary>
        /// Gets or sets the identity resources.
        /// </summary>
        /// <value>
        /// The identity resources.
        /// </value>
        public DbSet<IdentityResource> IdentityResources { get; set; }
        /// <summary>
        /// Gets or sets the API resources.
        /// </summary>
        /// <value>
        /// The API resources.
        /// </value>
        public DbSet<ApiResource> ApiResources { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Gets or sets the persisted grants.
        /// </summary>
        /// <value>
        /// The persisted grants.
        /// </value>
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        
        /// <summary>
        /// Gets or sets the device flowcodes.
        /// </summary>
        /// <value>
        /// The device flowcodes.
        /// </value>

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    }
}
