using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ataoge.EntityFrameworkCore
{
    public class AtaogeDbContext : DbContext, IAtaogeDbContext
    {
        public AtaogeDbContext(DbContextOptions<AtaogeDbContext> options)
            : base(options)
        {
            
        }

        protected AtaogeDbContext(DbContextOptions options)
            : base(options)
        {
            //InitializeDbContext();
        }

        ///<summary>
        ///For test only
        ///</summary>
        protected AtaogeDbContext()
        {

        }


        public string ProviderName { get { return Database.ProviderName;}}

        public virtual string ConvertName(string fromEntityName)
        {
            return AtaogeDbContext.ConvertName(ProviderName, fromEntityName);
       
        }

        internal static string ConvertName(string providerName, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentNullException(nameof(name));

            switch (providerName)
            {
                //case "MySql.Data.EntityFrameworkCore":
                case "Npgsql.EntityFrameworkCore.PostgreSQL":
                    return name.ToLower();
                case "Oracle":
                    return name.ToUpper();
                case "Microsoft.EntityFrameworkCore.Sqlite":
                default:
                    return name;
            }
        }
    }

  
}